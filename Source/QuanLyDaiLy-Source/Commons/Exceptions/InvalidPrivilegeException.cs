using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Commons.Exceptions
{
    /// <summary>
    /// Invalid Privilege Exception is thrown when the user tries to do something he/she isn't supposed to do
    /// within his/her permissions.
    /// </summary>
    [Serializable]
    class InvalidPrivilegeException : Exception
    {
        public InvalidPrivilegeException()
        {
            //Message += base.Message;
        }
        public InvalidPrivilegeException(string message) : base(message)
        {
            //Message += base.Message;
        }
        public InvalidPrivilegeException(string message, Exception inner) : base(message, inner)
        {
            //Message += base.Message;
        }
        
        public new string Message = "Chỉ có Admin mới có quyền hạn thay đổi mục này.";
    }
}
