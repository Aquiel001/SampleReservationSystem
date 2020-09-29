import {UserTypes} from './UserTypes';

export class Reservations {
    id: string;
    imageId: string;
    ranking: number;
    isFavorite: boolean;
    userId: string;
    contactName: string;
    birthDate: string;
    contactType = UserTypes;
    phoneNumber: string;
    details: string;
    reservations: {};
}
