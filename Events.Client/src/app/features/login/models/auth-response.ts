import { User } from "../../participant/models/user.model";

export interface AuthResponse {
    accessToken: string;
    refreshToken: string;
    user: User;
}