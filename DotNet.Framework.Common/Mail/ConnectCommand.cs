using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;

namespace DotNet.Framework.Common.Mail
{
    /// <summary>
    /// 连接到Pop3服务器执行并返回一个Pop3 
    /// 响应指示尝试连接的结果和 网络流中所有后续Pop3命令的使用。
    /// </summary>
    internal sealed class ConnectCommand : Pop3Command<ConnectResponse>
    {
        private TcpClient _client;
        private string _hostname;
        private int _port;
        private bool _useSsl;

        /// <summary>
        /// 初始化一个新的实例 <see cref="ConnectCommand"/> .
        /// </summary>
        /// <remarks>
        /// 即使网络流提供的基本构造流
        /// 已经不存在，所以我们要在一个虚拟的流发送，直到实际
        /// 连接已经发生。然后，我们将复位网络流
        /// 流TcpClient.GetStream（），可以读取返回的数据
        /// 后一个连接。
        /// </remarks>
        /// <param name="client">The client.</param>
        /// <param name="hostname">The hostname.</param>
        /// <param name="port">The port.</param>
        /// <param name="useSsl">if set to <c>true</c> [use SSL].</param>
        public ConnectCommand(TcpClient client, string hostname, int port, bool useSsl)
            : base(new System.IO.MemoryStream(), false, Pop3State.Unknown)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }

            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentNullException("hostname");
            }

            if (port < 1)
            {
                throw new ArgumentOutOfRangeException("port");
            }

            _client = client;
            _hostname = hostname;
            _port = port;
            _useSsl = useSsl;
        }

        /// <summary>
        /// Creates the connect request message.
        /// </summary>
        /// <returns>A byte[] containing connect request message.</returns>
        protected override byte[] CreateRequestMessage()
        {
            return null;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        internal override ConnectResponse Execute(Pop3State currentState)
        {
            EnsurePop3State(currentState);

            try
            {
                _client.Connect(_hostname, _port);

                SetClientStream();
            }
            catch (SocketException e)
            {
                throw new Pop3Exception(string.Format("无法连接到 {0}:{1}.", _hostname, _port), e);
            }

            return base.Execute(currentState);
        }

        /// <summary>
        /// Sets the client stream.
        /// </summary>
        private void SetClientStream()
        {
            if (_useSsl)
            {
                try
                {
                    NetworkStream = new SslStream(_client.GetStream(), true); //make sure the inner stream stays available for the Pop3Client to make use of.
                    ((SslStream)NetworkStream).AuthenticateAsClient(_hostname);
                }
                catch (ArgumentException e)
                {
                    throw new Pop3Exception("主机名: " + _hostname + "无法创建 SSL 流", e);
                }
                catch (AuthenticationException e)
                {
                    throw new Pop3Exception("主机名: " + _hostname + "无法验证SSL流", e);
                }
                catch (InvalidOperationException e)
                {
                    throw new Pop3Exception("试图为主机名: " + _hostname + "验证SSL流的时候出现了一个问题", e);
                }
            } //wrap NetworkStream in an SSL stream
            else
            {
                NetworkStream = _client.GetStream();
            }

        }

        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="buffer">The buffer.缓冲区</param>
        /// <returns>
        /// <c>Pop3Response</c> 包含POP3命令执行的结果.
        /// </returns>
        protected override ConnectResponse CreateResponse(byte[] buffer)
        {
            Pop3Response response = Pop3Response.CreateResponse(buffer);
            return new ConnectResponse(response, NetworkStream);
        }
    }
}
