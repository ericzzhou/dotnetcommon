
namespace DotNet.Framework.Common.Json
{
    /// <summary>
    /// Json操作类
    /// </summary>
    public class Json
    {
        /// <summary>
        /// 
        /// </summary>
        private JsonData _data;
        private int _error = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="val"></param>
        /// <param name="__type"></param>
        public Json(string val, string __type)
        {
            this._data = new JsonData(val, __type);
        }

        public JsonData data
        {
            get
            {
                return this._data;
            }
        }

        public int error
        {
            get
            {
                return this._error;
            }
            set
            {
                this._error = value;
            }
        }
    }
}
