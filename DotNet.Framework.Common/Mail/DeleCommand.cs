using System;
using System.Collections.Generic;
//using System.Net.Sockets;
using System.Text;
using System.IO;

namespace DotNet.Framework.Common.Mail
{
    /// <summary>
    /// POP3 删除命令
    /// This class represents the Pop3 DELE command.
    /// </summary>
    internal sealed class DeleCommand : Pop3Command<Pop3Response>
    {
        int _messageId = int.MinValue;

        /// <summary>
        /// 初始化一个新实例  of <see cref="DeleCommand"/> .
        /// </summary>
        /// <param name="stream">(流)The stream.</param>
        /// <param name="messageId">The message id.</param>
        public DeleCommand(Stream stream, int messageId)
            : base(stream, false, Pop3State.Transaction)
        {
            if (messageId < 0)
            {
                throw new ArgumentOutOfRangeException("_messageId");
            }
            _messageId = messageId;
        }

        /// <summary>
        /// Creates the DELE request message.
        /// </summary>
        /// <returns>
        /// The byte[] containing the DELE request message.
        /// </returns>
        protected override byte[] CreateRequestMessage()
        {
            return GetRequestMessage(string.Concat(Pop3Commands.Dele, _messageId.ToString(), Pop3Commands.Crlf));
        }
    }
}
