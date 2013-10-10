using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CodeforcesAddin
{
    [DataContract]
    public class SubmissionStatus
    {
        [DataMember]
        public string partyName { get; set; }
        [DataMember]
        public string contestName { get; set; }
        [DataMember]
        public string source { get; set; }
        [DataMember]
        public bool waiting { get; set; }
        [DataMember]
        public string verdict { get; set; }
        [DataMember]
        public string problemName { get; set; }
        [DataMember]
        public string href { get; set; }
        [DataMember]
        public bool compilationError { get; set; }
        [DataMember]
        public int testCount { get; set; }

        public List<SubmissionTestStatus> Tests { get; set; }
    }
    
    public class SubmissionTestStatus
    {
        public int Number { get; set; }
        public string Verdict { get; set; }
        public string Memory { get; set; }
        public string Time { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string ExitCode { get; set; }
        public string CheckerExitCode { get; set; }
        public string Answer { get; set; }
        public string checkerStdoutAndStderr { get; set; }
    }
}