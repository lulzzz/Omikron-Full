export declare interface ILogPayload
{
    /**
     * Name of the logger
     */
    name?: string;

    /**
     * Message to be delivered
     */
    message?: string;
    /**
     * Property bag to contain an extension to domain properties
     */
    properties?: {
        [key: string]: any;
    };

    /**
     * (Optional) Property bag to contain additional custom measurements
     */
    measurements?: {
        [key: string]: number;
    };
}
