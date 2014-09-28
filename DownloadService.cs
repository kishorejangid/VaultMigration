using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using VaultMigration.CWS;
using VaultMigration.Properties;

namespace VaultMigration
{
    public class DownloadService
    {
        private OTAuthentication _otAuth;
        private DocumentManagementClient _docManClient;
        private ContentService _contentService;
        private readonly List<Node> _nodes = new List<Node>();
        private readonly int _nodeID;
        private readonly string _downloadPath;        

        public event EventHandler<DownloadServiceEventArgs> NodesFetched;
        public event EventHandler<DownloadServiceEventArgs> CurrentDownloadChanged;
        public event EventHandler<DownloadProgressEventArgs> DownloadProgressChanged;

        public DownloadService(int nodeID,string downloadPath)
        {
            if (nodeID == 0)
                throw new ArgumentException("NodeID should be a valid ID");
            if(string.IsNullOrWhiteSpace(downloadPath))
                throw new ArgumentNullException("downloadPath",Resources.DownloadPathCannotBeNull);
            _nodeID = nodeID;
            _downloadPath = downloadPath;            
        }

        private string GenerateToken()
        {
            return new AuthenticationClient().AuthenticateUser(Settings.UserName, Settings.Password);
        }

        private void AuthenticateServices()
        {
            _otAuth = new OTAuthentication {AuthenticationToken = GenerateToken()};
            _docManClient = new DocumentManagementClient();
            _contentService = new ContentServiceClient();
        }

        public BlockingCollection<VaultObject> Queue { private get; set; }        
        
        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public void Download()
        {            
            AuthenticateServices();
            var node = _docManClient.GetNode(ref _otAuth, _nodeID);
            if (node == null)
            {
                throw new Exception(string.Format("Node {0} not found in Content Server.", _nodeID));                
            }
            if (node.IsContainer)
            {
                _nodes.AddRange(GetAllChildNodes(node));
            }
            else
            {
                _nodes.Add(node);
            }
            
            NodesFetched.Raise(this,new DownloadServiceEventArgs{NodeCount = _nodes.Count});
            int count = 0;
            foreach (var doc in _nodes)
            {
                count = count + 1;
                CurrentDownloadChanged.Raise(this,new DownloadServiceEventArgs{CurrentDownload = doc.Name,DownloadProgress = count});
                var path = GetPath(doc);
                var vaultObj = new VaultObject
                {
                    Name = doc.Name,
                    CreateTime = doc.CreateDate == null ? DateTime.Now : doc.CreateDate.Value,
                    Path = path
                };

                if (doc.IsContainer)
                {
                    Directory.CreateDirectory(Path.Combine(_downloadPath, path));
                }
                else
                {
                    var dir = Path.GetDirectoryName(Path.Combine(_downloadPath,path));
                    if (dir != null)
                    {
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);
                    }                    
                    var context = _docManClient.GetVersionContentsContext(ref _otAuth, doc.ID,
                        doc.VersionInfo.VersionNum);

                    var response =
                        _contentService.DownloadContent(new DownloadContentRequest(_otAuth, context));

                    using (
                        FileStream fileStream = new FileStream(Path.Combine(_downloadPath, path),
                            FileMode.Create, FileAccess.Write))
                    {
                        CopyStream(response.DownloadContentResult,fileStream);
                    }
                }

                if(Queue!=null)
                    Queue.Add(vaultObj);
            }
            if (Queue != null) Queue.CompleteAdding();
        }

        private string GetPath(Node node)
        {
            List<string> pathList = new List<string>();            
            var tempNode = node;
            while (tempNode!=null)
            {
                pathList.Add(tempNode.Name);
                tempNode = _nodes.SingleOrDefault(x => x.ID == tempNode.ParentID);                
            }
            pathList.Reverse();
            return string.Join(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture), pathList.ToArray());
        }

        private IEnumerable<Node> GetAllChildNodes(Node node)
        {
            var nodes =  _docManClient.ListNodes(ref _otAuth, node.ID,false);            
            if (nodes == null || nodes.Length <= 0) yield break;
            TotalNodes = TotalNodes + nodes.Length;            
            NodesFetched.Raise(this, new DownloadServiceEventArgs { NodeCount = TotalNodes });
            foreach (var childNode in nodes)
            {
                yield return childNode;
                
                if (!childNode.IsContainer) continue;
                foreach (var allChildNode in GetAllChildNodes(childNode))
                {
                    yield return allChildNode;
                }
            }
        }

        public int TotalNodes { get; set; }
    }
}
