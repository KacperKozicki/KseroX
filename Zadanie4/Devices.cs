using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4
{
    public interface IDevice
    {
        enum State { on, off, standby };

        void PowerOn();
        void PowerOff();
        void StandbyOn();
        void StandbyOff();
        State GetState();
    }

    public interface IPrinter : IDevice
    {
        void Print(in IDocument document);

        protected virtual void SetState(State state)
        {
            Console.WriteLine($"Printer state set to {state}");
        }
    }

    public interface IScanner : IDevice
    {
        void Scan(out IDocument document, IDocument.FormatType formatType);

        protected virtual void SetState(State state)
        {
            Console.WriteLine($"Scanner state set to {state}");
        }
    }

    public interface IDocument
    {
        enum FormatType { PDF, JPG, TXT };

        string GetFileName();
    }
}
