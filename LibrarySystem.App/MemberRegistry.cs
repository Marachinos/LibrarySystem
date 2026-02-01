using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.App
{
    public class MemberRegistry
    {
        private readonly List<Member> _members = new();

        public IReadOnlyList<Member> Members => _members.AsReadOnly();

        public void Add(Member member)
        {
            if (member is null) throw new ArgumentNullException(nameof(member));
            if (_members.Any(m => m.MemberId.Equals(member.MemberId, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("En medlem med samma MedlemsId finns redan registrerad.");
            
            _members.Add(member);
        }

        public Member? FindById(string memberId)
        {
            if (string.IsNullOrWhiteSpace(memberId)) return null;
            return _members.FirstOrDefault(m => m.MemberId.Equals(memberId, StringComparison.OrdinalIgnoreCase));
        }
    }
}
