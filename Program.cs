Console.WriteLine("Ready!");

Main.Run();

// this class is the pubhlisher
// after Start is called, it will wait
// 5 seconds then publish an event
class EmergencyAlertCenter {

    public event AlertEvent AlertHandler;

    public void Start() {
        // sleep for 5 seconds
        Thread.Sleep(5000);
        // custom data being send to all subscribers
        AlertArgs args = new AlertArgs {
            Code = 100, Message = $"The message is for code 100."
        };
        PublishEvent(args);
    }

    protected virtual void PublishEvent(AlertArgs args) {
        //if AlertHandler is not null then calls all delegates
        Console.WriteLine("Publish event ...");
        // publish message to all subscribers
        AlertHandler?.DynamicInvoke(null, args);
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
        eac.AlertHandler += AlertHandler1;
        eac.AlertHandler += AlertHandler2;
        eac.Start();

    }
    // first handler to receive the message
    public static void AlertHandler1(object sender, AlertArgs args) {
        Console.WriteLine($"Handler1 ... Code[{args.Code}], Message[{args.Message}]");
    }
    // second handler to receive the message
    public static void AlertHandler2(object sender, AlertArgs args) {
        Console.WriteLine($"Handler2 ... ");
    }
}

// thid defines what signature all handlers must have
delegate void AlertEvent(object sender, AlertArgs args);

// this class defines the custom data included
// in the published message
class AlertArgs : EventArgs {

    public int Code { get; set; } = 0;
    public string Message { get; set; } = null!;

}

