using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.App
{
    public interface ISearchable
    {
        bool Matches(string searchTerm);
    }
}
