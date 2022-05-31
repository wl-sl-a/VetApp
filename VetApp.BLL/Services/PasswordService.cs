using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using VetApp.Core.Services;

namespace VetApp.BLL.Services
{
    public class PasswordService : IPasswordService
    {
        private const string PasswordCharsLcase = "abcdefgijkmnpqrstwxyz";
        private const string PasswordCharsUcase = "ABCDEFGHJKLMNPQRSTWXYZ";
        private const string PasswordCharsNumeric = "1234567890";
        private const string PasswordCharsSymbol = "_";
        private const string PasswordCharsSpecial = "ABCDEFGHJKLMNPQRSTWXYZabcdefgijkmnpqrstwxyz1234567890_";

        
        public string GeneratePassword(int minLength, int maxLength)
        {
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
            {
                return null;
            }
            
            var charGroups = new[]
            {
                PasswordCharsLcase.ToCharArray(),
                PasswordCharsUcase.ToCharArray(),
                PasswordCharsNumeric.ToCharArray(),
                PasswordCharsSpecial.ToCharArray(),
                PasswordCharsSymbol.ToCharArray()
            };

            var charsLeftInGroup = new int[charGroups.Length];

            for (var i = 0; i < charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }

            var leftGroupsOrder = new int[charGroups.Length];

            for (int i = 0; i < leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i] = i;
            }

            var randomBytes = new byte[4];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            var seed = (randomBytes[0] & 0x7f) << 24 |
                       randomBytes[1] << 16 |
                       randomBytes[2] << 8 |
                       randomBytes[3];

            var random = new Random(seed);

            var password = minLength < maxLength ? new char[random.Next(minLength, maxLength + 1)] : new char[minLength];

            var lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            for (var i = 0; i < password.Length; i++)
            {
                var nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0 ? 0 : random.Next(0, lastLeftGroupsOrderIdx);

                var nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];
                var lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                var nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);

                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                if (lastCharIdx == 0)
                {
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
                }
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        var temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    charsLeftInGroup[nextGroupIdx]--;
                }

                if (lastLeftGroupsOrderIdx == 0)
                {
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                }
                else
                {
            
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        var temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }

                    lastLeftGroupsOrderIdx--;
                }
            }

            return new string(password);
        }
    }
}
