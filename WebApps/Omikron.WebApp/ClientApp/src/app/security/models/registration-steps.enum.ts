export enum RegistrationSteps {
    CreateAccount,
    SMSVerification,
    MyDetails,
    MyAccount
}

export class Registration{
    activeStep: RegistrationSteps = RegistrationSteps.CreateAccount;
    passedStep: RegistrationSteps = RegistrationSteps.CreateAccount;
    accountCreated: boolean;
}
