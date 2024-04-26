using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DES
{
    internal class DES
    {
        public static int[,,] arrSBox = new int[8, 4, 16]
        {
                    {
                        {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
                        {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
                        {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
                        {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
                    },
                    {
                        {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                        {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                        {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                        {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
                    },
                    {
                        {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                        {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                        {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                        {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
                    },
                    {
                        {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                        {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                        {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                        {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
                    },
                    {
                        {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                        {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                        {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                        {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
                    },
                    {
                        {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                        {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                        {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                        {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}
                    },
                    {
                        {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                        {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                        {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                        {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}
                    },
                    {
                        {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                        {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                        {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                        {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
                    }
        };

        public static int[] arrIP = {
                            58, 50, 42, 34, 26, 18, 10, 2,
                            60, 52, 44, 36, 28, 20, 12, 4,
                            62, 54, 46, 38, 30, 22, 14, 6,
                            64, 56, 48, 40, 32, 24, 16, 8,
                            57, 49, 41, 33, 25, 17, 9, 1,
                            59, 51, 43, 35, 27, 19, 11, 3,
                            61, 53, 45, 37, 29, 21, 13, 5,
                            63, 55, 47, 39, 31, 23, 15, 7
                        };

        public static int[] EBitSelectionTable = {
                                        32, 1, 2, 3, 4, 5,
                                        4, 5, 6, 7, 8, 9,
                                        8, 9, 10, 11, 12, 13,
                                        12, 13, 14, 15, 16, 17,
                                        16, 17, 18, 19, 20, 21,
                                        20, 21, 22, 23, 24, 25,
                                        24, 25, 26, 27, 28, 29,
                                        28, 29, 30, 31, 32, 1
                                    };

        public static int[] arrP = {
                                16, 7, 20, 21,
                                29, 12, 28, 17,
                                1, 15, 23, 26,
                                5, 18, 31, 10,
                                2, 8, 24, 14,
                                32, 27, 3, 9,
                                19, 13, 30, 6,
                                22, 11, 4, 25
                            };

        public static int[] arrFinalyP = new int[]
        {
            40, 8, 48, 16, 56, 24, 64, 32,
            39, 7, 47, 15, 55, 23, 63, 31,
            38, 6, 46, 14, 54, 22, 62, 30,
            37, 5, 45, 13, 53, 21, 61, 29,
            36, 4, 44, 12, 52, 20, 60, 28,
            35, 3, 43, 11, 51, 19, 59, 27,
            34, 2, 42, 10, 50, 18, 58, 26,
            33, 1, 41, 9, 49, 17, 57, 25

        };

        public static int[] arrPC1 =
        {
            57, 49, 41, 33, 25, 17, 9,
            1, 58, 50, 42, 34, 26, 18,
            10, 2, 59, 51, 43, 35, 27,
            19, 11, 3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
            7, 62, 54, 46, 38, 30, 22,
            14, 6, 61, 53, 45, 37, 29,
            21, 13, 5, 28, 20, 12, 4
        };

        public static int[] arrPC2 = new int[]
        {
            14, 17, 11, 24, 1, 5,
            3, 28, 15, 6, 21, 10,
            23, 19, 12, 4, 26, 8,
            16, 7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        public static bool LengthValidate(string txt) => txt.Length % 4 != 0;

        public static string HexToBinary(string txtHex)
        {
            Dictionary<char, string> dicHexToBinary = new Dictionary<char, string>
            {
              // Hex   Binary
                {'0', "0000"},
                {'1', "0001"},
                {'2', "0010"},
                {'3', "0011"},
                {'4', "0100"},
                {'5', "0101"},
                {'6', "0110"},
                {'7', "0111"},
                {'8', "1000"},
                {'9', "1001"},
                {'A', "1010"},
                {'B', "1011"},
                {'C', "1100"},
                {'D', "1101"},
                {'E', "1110"},
                {'F', "1111"}
            };

            string strBinary = string.Empty;
            byte counter = 0;

            foreach (char character in txtHex)
            {
                // Hexdecimal to Binary
                strBinary = strBinary + dicHexToBinary[txtHex[counter++]];  
            }

            return strBinary;
        }

        public static string SetBinaryWidth(string txtBinary, int Width) => txtBinary.PadLeft((Width - txtBinary.Length % Width), '0');
        
        public static string BinaryToHex(string txtBinary)
        {
            // Ikili sayının hanelerin sayıyı dörde değilse kontrol et ve dört bitlik şeklide yap
            // 01 -> 0001
            if (LengthValidate(txtBinary)) 
                txtBinary =  SetBinaryWidth(txtBinary, 4);

            Dictionary<string, string> dicHexToBinary = new Dictionary<string, string>
            {
              // Binary  Hex
                {"0000", "0"},
                {"0001", "1"},
                {"0010", "2"},
                {"0011", "3"},
                {"0100", "4"},
                {"0101", "5"},
                {"0110", "6"},
                {"0111", "7"},
                {"1000", "8"},
                {"1001", "9"},
                {"1010", "A"},
                {"1011", "B"},
                {"1100", "C"},
                {"1101", "D"},
                {"1110", "E"},
                {"1111", "F"}
            };

            string strBinary = string.Empty;
            string strHex = string.Empty;

            byte counter = 0;
            for (int i = 0; i < txtBinary.Length; i++)
            {
                strBinary = strBinary + txtBinary[i];

                // Her dört biti hex'a dönüştürülüyor
                if (++counter == 4)
                {
                    strHex = strHex + dicHexToBinary[strBinary];
                    strBinary = "";
                    counter = 0;
                }
            }

            return strHex;
        }

        public static string Shuffle(string key, int[] arr)
        {
            // Gelen binary anahtarı gelen diziye göre(IP. IP^-1,  E BIT-SELECTION TABLE, PC-2, PC-1) karıştırıyor.
            string strKey = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                // item 64 = index 64 - 1
                strKey = strKey + key[arr[i] - 1];
            }

            return strKey;
        }

        public static List<string> DivideTextToNChunks(string txt, int NumberOfchunks)
        {
            List<string> lstChunksOfKey = new List<string>();
            int chunkLength = txt.Length / NumberOfchunks;
            for (int i = 0; i < NumberOfchunks; i++)
            {
                /*
                 *  gelen metin belli parçalara parçalayıp listeye ekleniyor.
                 *  Example: 
                 *  M = 0000 0001 0010 0011 0100 0101 0110 0111 1000 1001 1010 1011 1100 1101 1110 1111 -> txt, NumberOfChunks = 2
                 *  L = 0000 0001 0010 0011 0100 0101 0110 0111 => first chunk
                 *  R = 1000 1001 1010 1011 1100 1101 1110 1111 => second chunk
                */
                lstChunksOfKey.Add(txt.Substring(i * chunkLength, chunkLength));
            }

            return lstChunksOfKey;
        }

        public static void DividList(List<string> superList, ref List<string> lst1, ref List<string> lst2)
        {
            // Bir listeyi iki listeye bölünüyor
            // C1 D1, C2 D2 .... 
            lst1.AddRange(superList.Where((a, b) => b % 2 == 0));
            lst2.AddRange(superList.Where((a, b) => b % 2 == 1));
        }

        public static string ShiftBits(string BinaryString, int NumberOfBits)
        {
            // gelen binary metni NumberOfBits sayısına kadar kaydırılıyor
            string ShiftedString = BinaryString.Substring(NumberOfBits) + BinaryString.Substring(0, NumberOfBits);
            return ShiftedString;
        }

        public static void ShiftAllCases(List<string> lstChuncksOfKey, ref List<string> lstC, ref List<string> lstD)
        {
            // Listeyi iki listeye bölündü (C ve D)
            DividList(lstChuncksOfKey, ref lstC, ref lstD);

            int index = 0;
            while (index < 16)
            {
                if (index != 0 && index != 1 && index != 8 && index != 15)
                {
                    lstC.Add(ShiftBits(lstC[index], 2));
                    lstD.Add(ShiftBits(lstD[index], 2));
                }
                else
                {
                    // 1, 2, 9, 16 olan anahtarlar 1 bit kaydırılıyor diğerleri ise 2 bit.
                    lstC.Add(ShiftBits(lstC[index], 1));
                    lstD.Add(ShiftBits(lstD[index], 1));
                }

                index++;
            }
        }

        public static List<string> MergeChunksOfKey(List<string> lstC, List<string> lstD)
        {
            // C1 ve D1 anahtarları birleştilen fonksiyoun
            List<string> lstKeys = new List<string>();
            for (int i = 1; i < lstC.Count; i++)
            {
                lstKeys.Add(lstC[i] + lstD[i]);
            }

            return lstKeys;
        }

        public static List<string> AllKeysFrom56To48(List<string> lstKeys)
        {
            // C ve D'yi birleşiminden oluşan 56 bitlik anahtarları 48 bit'e dönüştürülüyor.
            List<string> lstKeysOf48bits = new List<string>();
            for (int i = 0; i < lstKeys.Count; i++)
            {
                lstKeysOf48bits.Add(Shuffle(lstKeys[i], arrPC2));
            }

            return lstKeysOf48bits;
        }

        public static string XOR(string strBinary1, string strBinray2)
        {
            // Gelen ikili sayilarinin uzunluklari bir birine esit degilse uzunluklar esit yapiliyor
            if (strBinary1.Length > strBinray2.Length)
                strBinray2 = SetBinaryWidth(strBinray2, strBinary1.Length - strBinray2.Length);

            else if (strBinray2.Length > strBinary1.Length)
                strBinary1 = SetBinaryWidth(strBinary1, strBinray2.Length - strBinary1.Length);
                

            string Result = "";

            for (int i = 0; i < strBinary1.Length; i++)
            {
                Result = Result + (strBinary1[i] == strBinray2[i] ? "0" : "1");
            }

            return Result;
        }

        public static int Power(int Base, int Power)
        {
            // Üs alama 
            if (Base == 1 || Power == 0) return 1;
            if (Base == 0 && Power != 0) return 0;

            int Result = 1;

            for (int i = 0; i < Power; i++)
            {
                Result *= Base;
            }

            return Result;
        }

        public static int BinaryToDecimal(string strBinary)
        {
            // ikili sayı sisteminde ondalık sayıya 

            strBinary = Reverse(strBinary);
            int Number = 0;
            int n;
            for (int i = 0; i < strBinary.Length; i++)
            {
                n = Convert.ToInt32(strBinary[i].ToString());
                Number = Number + n * Power(2, i);
            }
            return Number;
        }

        public static string DecimalToBinary(int Number, int TotalWith = 4)
        {
            // Ondalık sisteminde ikili sisteme
            if (Number == 0)
                return "0".PadLeft(TotalWith, '0');

            string strBinaryNumber = string.Empty;
            int Remainder = 0;
            while (Number > 0)
            {
                Remainder = Number % 2;
                strBinaryNumber = strBinaryNumber + Remainder.ToString();
                Number = Number / 2;
            }

            return Reverse(strBinaryNumber).PadLeft(TotalWith, '0');
        }

        public static string Reverse(string str)
        {
            string ReversedString = string.Empty;

            for (int i = str.Length - 1; i >= 0; ReversedString += str[i--]) ;

            return ReversedString;
        }

        public static string SBox(List<string> lstBinaryChunks)
        {
            string strBoxedChunks = string.Empty;
            int sBoxRow = 0, sBoxCol = 0;

            for (int i = 0; i < lstBinaryChunks.Count; i++)
            {
                /*
                 * 101001
                 * 11   -> 3 satir
                 * 0100 -> 4 sutun
                 * for first box  => 1
                 * arrSBox[1, 3, 4] = 11 => 1011 for example
                 * strBoxedChunks = 1011
                 * 8 bitlik anahtari 6 bit'e dönüştürülüyor.
                 */

                sBoxRow = BinaryToDecimal(lstBinaryChunks[i].Substring(0, 1) + lstBinaryChunks[i].Substring(lstBinaryChunks[i].Length - 1));
                sBoxCol = BinaryToDecimal(lstBinaryChunks[i].Substring(1, lstBinaryChunks[i].Length - 2));
                strBoxedChunks = strBoxedChunks + DecimalToBinary(arrSBox[i, sBoxRow, sBoxCol], 4);
            }

            return strBoxedChunks;
        }

        public static string f(string R0, string K1)
        {
            R0 = Shuffle(R0, EBitSelectionTable);

            //K1 + E(R0) = 011000 010001 011110 111010 100001 100110 010100 100111.
            string RxorK = XOR(R0, K1);

            // K1 + E(R0) sonucundan olusan anahtarı 8 bitlik şekinde parçlarak bir listeye ekleniyor.
            List<string> lstBinaryChunksOf8bits = new List<string>();
            lstBinaryChunksOf8bits = DivideTextToNChunks(RxorK, 8);

            // 8 bitlik parçaları SBox giriliyor
            string str = SBox(lstBinaryChunksOf8bits);

            // SBox'a girilen anahatrı arrP tablosuna giriliyor.
            return Shuffle(str, arrP);

        }

        public static string KeyAndMsgIter(List<string> lstIP, List<string> lstKeys)
        {
            /*
                M = 0000 0001 0010 0011 0100 0101 0110 0111 1000 1001 1010 1011 1100 1101 1110 1111
                L = 0000 0001 0010 0011 0100 0101 0110 0111
                R = 1000 1001 1010 1011 1100 1101 1110 1111
                
                K1 = 000110 110000 001011 101111 111111 000111 000001 110010
                K2 = 011110 011010 111011 011001 110110 111100 100111 100101
                K3 = 010101 011111 110010 001010 010000 101100 111110 011001
                K4 = 011100 101010 110111 010110 110110 110011 010100 011101
                
                Ln = Rn-1
                Rn = Ln-1 + f(Rn-1,Kn)
                
                K1 =      000110 110000 001011 101111 111111 000111 000001 110010
                L1 = R0 = 1111 0000 1010 1010 1111 0000 1010 1010
                R1 = L0 + f(R0,K1)
                
                f(R0,K1) => E BIT-SELECTION TABLE using to convert R0 from 32bit to 48bit
                f(R0,K1) => K1+E(R0) = 011000 010001 011110 111010 100001 100110 010100 100111.
                =>
                   After this step we must enter each piece of this result into the S-BOX to convert the result 
                   from 48 bits to 32 bits.
                
                R1 = L0 + f(R0 , K1 )
                   = 1100 1100 0000 0000 1100 1100 1111 1111
                   + 0010 0011 0100 1010 1010 1001 1011 1011
                   = 1110 1111 0100 1010 0110 0101 0100 0100

                n=1
                    L1=R0
                    R1=L0+F(R0,K1)

                n=2
                    L2=R1
                    R2=L1+F(R1,K2)

                n=3
                    L3=R2
                    R3=L2+F(R2,K3)
                .
                .
                .

                n=16
                    L16=R15
                    R16=L15+F(R15,K16)
                
            */

            List<string> lstLeft = new List<string>();
            List<string> lstRight = new List<string>();

            DividList(lstIP, ref lstLeft, ref lstRight);

            string L0, L1, R0, R1, K1;
            K1 = L0 = L1 = R0 = R1 = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                K1 = lstKeys[i];
                L0 = lstLeft[i];
                L1 = R0 = lstRight[i];
                R1 = XOR(L0, f(R0, K1));

                lstLeft.Add(L1);
                lstRight.Add(R1);
            }

            return lstRight[16] + lstLeft[16];
        }

        static void Main(string[] args)
        {
            string Message = "0123456789ABCDEF";
            Console.WriteLine($"Message: {Message}");
            string strBinaryMessage = HexToBinary(Message);
            Console.WriteLine($"My massege \"{Message}\" after converting it to a binary: " + strBinaryMessage);
            List<string> lstBinaryMessageLR = new List<string>();
            lstBinaryMessageLR = DivideTextToNChunks(strBinaryMessage, 2);
            Console.WriteLine("L = " + lstBinaryMessageLR[0]);
            Console.WriteLine("R = " + lstBinaryMessageLR[1]);
            Console.WriteLine("\n");



            List<string> lstBinaryKeyChunks = new List<string>();
            string Key = "133457799BBCDFF1";
            string strBinaryKey = HexToBinary(Key);
            Console.WriteLine($"Key: {Key}");
            Console.WriteLine($"My key \"{Key}\" after converting it to a binary: " + strBinaryKey);
            lstBinaryKeyChunks = DivideTextToNChunks(Shuffle(strBinaryKey, arrPC1), 2);
            Console.WriteLine("Key L = " + lstBinaryKeyChunks[0]);
            Console.WriteLine("Key R = " + lstBinaryKeyChunks[1]);
            Console.WriteLine("\n");



            List<string> lstC = new List<string>();
            List<string> lstD = new List<string>();

            ShiftAllCases(lstBinaryKeyChunks, ref lstC, ref lstD);

            for (int i = 0; i < lstC.Count; i++)
            {
                Console.WriteLine($"C{i} {lstC[i]}");
                Console.WriteLine($"D{i} {lstD[i]}\n");
            }
            Console.WriteLine("\n");



            Console.WriteLine("All keys of 48 bits: ");
            List<string> lstKeysOf56bit = new List<string>();
            List<string> lstKeysOf48bit = new List<string>();
            lstKeysOf56bit = MergeChunksOfKey(lstC, lstD);
            int Counter = 0;
            Console.WriteLine();
            lstKeysOf48bit = AllKeysFrom56To48(lstKeysOf56bit);
            lstKeysOf48bit.ForEach(n => Console.WriteLine($"K" + (++Counter).ToString().PadRight(3) + "=  " + n));
            Console.WriteLine("\n");



            List<string> lstIP = new List<string>();
            string strIP = Shuffle(strBinaryMessage, arrIP);
            Console.WriteLine("Msg   : " + strBinaryMessage);
            Console.WriteLine("strIP : " + strIP + "\n");
            lstIP = DivideTextToNChunks(strIP, 2);
            Console.WriteLine("L = " + lstIP[0]);
            Console.WriteLine("R = " + lstIP[1]);
            string R16L16 = KeyAndMsgIter(lstIP, lstKeysOf48bit);

            Console.WriteLine("\nR16L16             : " + R16L16);
            string FinalyPermutation = Shuffle(R16L16, arrFinalyP);
            Console.WriteLine("Finaly Permutation : " + FinalyPermutation);
            Console.WriteLine("\n");

            string EncryptedMessage = BinaryToHex(FinalyPermutation);
            Console.WriteLine($"Message           : {Message}");
            Console.WriteLine($"Encrypted Message : {EncryptedMessage}");

            Console.ReadKey();

        }


    }
}
