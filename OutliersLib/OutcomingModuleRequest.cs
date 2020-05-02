using System.Collections.Generic;

namespace OutliersLib
{
    /// <summary>
    /// Модель посылки информации модулю
    /// </summary>
    public class OutcomingModuleRequest
    {
        public object Data { get; }
        public Dictionary<string, object> Params { get; }

        public OutcomingModuleRequest(object data, Dictionary<string, object> pars)
        {
            Data = data;
            Params = pars;
        }
    }
}