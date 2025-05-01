using System;

namespace Domain.Enums
{
    [Flags] // Bitwise işlemler için
    public enum LoginMethod : byte
    {
        Username = 1,  // 0001
        Email = 2,     // 0010
        Phone = 4,     // 0100
    }
}
