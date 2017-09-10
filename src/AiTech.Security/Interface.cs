using System;

namespace AiTech
{
    interface IRecordInfoAttribute
    {
        string CreatedBy { get; set; }
        DateTime Created { get; }

        string ModifiedBy { get; set; }
        DateTime Modified { get; }
    }
}
