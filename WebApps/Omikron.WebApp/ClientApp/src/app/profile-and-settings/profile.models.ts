export interface ProfileDetailsViewModel {
    nickname: string;
    email: string;
    phoneNumber: string;
    marketingCommunications?: boolean;
    accountNotifications?: boolean;
}

export interface GenerateTokenForNewNumberCommand {
    userId: string;
    phoneNumber: string;
}

export interface EditProfileDetailsCommand extends GenerateTokenForNewNumberCommand {
    nickname?: string;
    email?: string;
    verificationToken?: number;
}

export interface GenerateTokenToChangePasswordCommand {
    currentPassword: string;
}

export interface PasswordChangeCommand {
    currentPassword: string;
    newPassword: string;
    verificationToken?: number;
}