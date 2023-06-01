using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop.Services.Hentai
{
    public abstract class HentaiService
    {
        public  abstract Task Recent();
        public  abstract Task Popular();
        public  abstract Task Search();
    }
}
