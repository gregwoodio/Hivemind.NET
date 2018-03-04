export class LoginResponse {
    success: boolean;
    message: string;

    constructor(partial: Partial<LoginResponse>) {
        if (partial.message) {
            this.message = partial.message;
        }

        if (partial.success) {
            this.success = partial.success;
        }
    }
}
