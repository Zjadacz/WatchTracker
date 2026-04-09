import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@app/core/services/auth.service';

@Component({
    selector: 'app-confirm',
    templateUrl: './confirm.component.html',
    styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent implements OnInit {
    message = '';       
    
    constructor(
        private auth: AuthService, 
        private route: ActivatedRoute,
        private cdRef: ChangeDetectorRef) {}


    ngOnInit() {
        this.route.queryParamMap.subscribe(params => {
            const userId = params.get('userId');
            const token = params.get('token');
            if (token && userId) {
                this.auth.confirmEmail(userId, token).subscribe({
                    next: () => {
                        console.log('SUCCESS');
                        this.message = 'Account confirmed! You can now log in.';
                        this.cdRef.markForCheck();
                    },
                    error: (err) => {
                        this.message = err.error || 'Error confirming account.';
                        this.cdRef.markForCheck();
                    }
                });
            } else {
                this.message = 'Wrong confirmation link.';
            }
        });
    }
}