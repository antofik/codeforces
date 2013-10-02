// Guids.cs
// MUST match guids.h
using System;

namespace CodeforcesAddin
{
    static class GuidList
    {
        public const string guidCodeforcesAddinPkgString = "4ccbba3b-b309-4c55-a326-ae9b43aa9dcf";
        public const string guidCodeforcesAddinCmdSetString = "8fb96257-7c23-4239-acb3-cec161d2b413";
        public const string guidToolWindowPersistanceString = "a7427436-be60-4efc-865c-701a8347e9d4";
        public const string guidCodeforcesAddinEditorFactoryString = "55b9271f-477c-489d-813c-3e57eb83e6cb";

        public static readonly Guid Default = new Guid(guidCodeforcesAddinCmdSetString);
        public static readonly Guid guidCodeforcesAddinEditorFactory = new Guid(guidCodeforcesAddinEditorFactoryString);
    };
}