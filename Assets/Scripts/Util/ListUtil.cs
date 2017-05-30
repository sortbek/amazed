using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Util {
    public static class ListUtil<T> {

        public static List<T> Slice(List<T> inputList, int startIndex, int endIndex) {
            int elementCount = endIndex - startIndex + 1;
            return inputList.Skip(startIndex).Take(elementCount).ToList();
        }

    }
}
