using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using Metalama.Framework.Aspects;
using System.Diagnostics;

//public class LoggingAspect : OverrideMethodAspect
//{
//    public override dynamic OverrideMethod()
//    {
//        Debug.WriteLine($"{meta.Target.Method} started.");

//        try
//        {
//            var result = meta.Proceed();

//            Debug.WriteLine($"{meta.Target.Method} succeeded.");

//            return result;
//        }
//        catch (Exception e)
//        {
//            Debug.WriteLine($"{meta.Target.Method} failed: {e.Message}.");

//            throw;
//        }
//    }
//}

[PSerializable]
public class LoggingAspect : OnMethodBoundaryAspect
{

    public override void OnEntry(MethodExecutionArgs args)
    {
        Debug.WriteLine($"The {args.Method.Name} method has been entered.");
    }

    public override void OnSuccess(MethodExecutionArgs args)
    {
        Debug.WriteLine($"The {args.Method.Name} method executed successfully.");
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        Debug.WriteLine($"The {args.Method.Name} method has exited.");
    }

    public override void OnException(MethodExecutionArgs args)
    {
        Debug.WriteLine($"An exception was thrown in {args.Method.Name}.");
    }

}