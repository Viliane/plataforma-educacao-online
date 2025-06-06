﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacaoOnline.GestaoAluno.Domain
{
    public interface ICertificadoPdfGenerator
    {
        byte[] GerarPdf(Certificado certificado);
    }
}
