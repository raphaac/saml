using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrieSSO.Domain.Models
{
    public class SamlRequest
    {
        public string Id;
        public string IssueInstant;
        public string AssertionConsumerServiceEndPoint { get; set; }
        public string RelyingPartyIdentifier { get; set; }
        
        public SamlRequest(string id, string issueInstant, string assertionConsumerServiceEndPoint, string relyingPartyIdentifier)
        {
            Id = id;
            IssueInstant = issueInstant;
            AssertionConsumerServiceEndPoint = assertionConsumerServiceEndPoint;
            RelyingPartyIdentifier = relyingPartyIdentifier;
        }
    }
}
