using System;

namespace DotNet.Framework.Common.Json
{
    /// <summary>
    /// JsonData  Json数据处理
    /// </summary>
    public class JsonData
    {
        private string _data;
        private string _type;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="__data"></param>
        /// <param name="__type"></param>
        public JsonData(string __data, string __type)
        {
            this._data = __data;
            this._type = __type;
        }

        public string data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
            }
        }

        public string type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}
