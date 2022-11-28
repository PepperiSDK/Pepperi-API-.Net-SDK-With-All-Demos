using Pepperi.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Helpers
{
    internal static class ValuesValidator
    {
        internal static bool Validate<T>(T value, string message, bool isAddAdditionMessage = true) where T : class
        {
            if (value == null)
                throw new PepperiException(GeneralMessage(message, isAddAdditionMessage));
            return true;
        }

        internal static bool Validate(int value, string message, bool isAddAdditionMessage = true)
        {
            if (value == 0)
                throw new PepperiException(GeneralMessage(message, isAddAdditionMessage));
            return true;
        }

        internal static bool Validate(bool value, string message, bool isAddAdditionMessage = true)
        {
            if (!value)
                throw new PepperiException(GeneralMessage(message, isAddAdditionMessage));
            return true;
        }

        internal static bool Validate(string value, string message, bool isAddAdditionMessage = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new PepperiException(GeneralMessage(message, isAddAdditionMessage));
            return true;
        }

        internal static bool ValidateGuid(string guid, string message, bool isAddAdditionMessage = true)
        {
            var isCorrect = Guid.TryParse(guid, out var guidOutput);
            if (!isCorrect)
            {
                throw new PepperiException(GeneralMessage(message, isAddAdditionMessage));
            }
            return true;
        }
        internal static bool ValidateSameValue(IEnumerable<int> array, string message, bool isAddAdditionMessage = true)
        {
            Validate(array != null && array.Count() > 0, message, isAddAdditionMessage);
            var distinct = array.Distinct();
            Validate(distinct.Count() == 1, message, isAddAdditionMessage);
            return true;
        }

        private static string GeneralMessage(string message, bool isAddAdditionMessage = true)
        {
            return string.Format("{0}{1}", message, isAddAdditionMessage ? "" : "");
        }

    }
}
