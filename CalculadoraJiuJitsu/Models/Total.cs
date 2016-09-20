using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculadoraJiuJitsu.Models
{
    public class Total
    {
        public List<Models.classe> faixas { get; set; }
        public List<Models.classe> tempos { get; set; }
        public List<Models.classe> modalidades { get; set; }
        public List<Models.classe> lugares { get; set; }
    }
}