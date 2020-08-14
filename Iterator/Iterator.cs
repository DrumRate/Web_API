using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Iterator
{
    public class Iterator : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return i;
            }

            for (int i = 100; i >= 0; i--)
            {
                yield return i;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return i;
            }

            for (int i = 100; i >= 0; i--)
            {
                yield return i;
            }
        }
    }
}
