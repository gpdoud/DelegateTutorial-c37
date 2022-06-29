Console.WriteLine("Ready!");

Main.Run();

// this class is the pubhlisher
// after Start is called, it will wait
// 5 seconds then publish an event
class EmergencyAlertCenter {

    public static event AlertEvent AlertHandler;

    public void Start() {
        // sleep for 5 seconds
        Console.WriteLine("EAC: Sleep for 5 seconds ...");
        Thread.Sleep(5000);
        // custom data being send to all subscribers
        AlertArgs args = new AlertArgs {
            Code = 100, Message = $"The message is for code 100."
        };
        Console.WriteLine("EAC: Publish the event ...");
        PublishEvent(args);
        // sleep for another 5 seconds.
        Console.WriteLine("EAC: Sleep for another 5 seconds ...");
        Thread.Sleep(5000);
    }

    private void PublishEvent(AlertArgs args) {
        //if AlertHandler is not null then calls all delegates
        // publish message to all subscribers
        AlertHandler?.Invoke(null!, args);
    }

}

// this class is the subscriber
// it has two delegates that will 
// receive the published message
class Main {

    public static void Run() {

        // create an instance of the publisher
        EmergencyAlertCenter eac = new();
        // subscribe to the message using
        // both handlers
        
        Console.WriteLine("Main: Start EAC as an async task ...");
        Task t = Task.Run(eac.Start);

        // Attach two subscriber methods while EAC is running
        Console.WriteLine("Main: Attach two event handlers ...");
        EmergencyAlertCenter.AlertHandler += AlertHandler1;
        EmergencyAlertCenter.AlertHandler += AlertHandler2;

        // wait until EAC ends
        Console.WriteLine("Main: Wait till EAC ends ...");
        // waits until the task is done
        t.Wait();
        Console.WriteLine("Main: Done ...");
    }
    // first handler to receive the message
    public static void AlertHandler1(object sender, AlertArgs args) {
        Console.WriteLine($"Main: Handler1 ... Code[{args.Code}], Message[{args.Message}]");
    }
    // second handler to receive the message
    public static void AlertHandler2(object sender, AlertArgs args) {
        Console.WriteLine($"Main: Handler2 ... Doesn't need the extra data");
    }
}

// third defines what signature all handlers must have
delegate void AlertEvent(object sender, AlertArgs args);

// this class defines the custom data included
// in the published message
class AlertArgs : EventArgs {

    public int Code { get; set; } = 0;
    public string Message { get; set; } = null!;

}

