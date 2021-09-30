using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandboxCodeEditor.Service
{
    public class PointerBuffer
    {
        public int Length { get; private set; }
        private List<int> indexes = new List<int>();

        public void Add(int pointer)
        {
            indexes.Add(pointer);
        }

        public void ChangePointer(int oldPointer, int newPointer)
        {
            throw new NotImplementedException();
        }
    }
}
