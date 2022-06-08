import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { ProfileDetailsViewModel } from "./profile.models";

@Injectable({ providedIn: 'root' })
export class DetailsUpdatedService {
    private profileDetailsSource = new Subject<ProfileDetailsViewModel>();

    detailsUpdated$ = this.profileDetailsSource.asObservable();

    updateDetails(profileDetails: ProfileDetailsViewModel){
        this.profileDetailsSource.next(profileDetails);
    }

}