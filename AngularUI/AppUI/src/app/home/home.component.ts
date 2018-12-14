import { Component, OnInit } from '@angular/core';

import { UserContentService } from '../_services';
import { UserContentList } from '../_models/userContentModelExtensions';

@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html',
    styleUrls: ['home.component.css']
})

export class HomeComponent implements OnInit {
    lblVideoName: string;
    videoId: string;
    player: YT.Player;

    usercontentlist: UserContentList[];
    defaultusercontent: UserContentList;
    selectedContentId = 0;

    contentCompletedCount = 0;
    contentCompletedPercent = 0;
    totalContents = 0;

    
    constructor(private userContentService: UserContentService) { }

    ngOnInit(): void {        
        this.getLoggedinUserContent();
    }

    // Runs when Youtube player is ready
    readyPlayer(player) {
        this.player = player;

        //When player is ready set default content list
        if (this.defaultusercontent) {
            this.onUserContentClick(this.defaultusercontent);
        }
    }

    // Check state of youtube player
    onStateChange(event) {
        //Check if player state has ended.. mark video complete
        if (event.data == YT.PlayerState.ENDED) {            

            for (let i in this.usercontentlist) {
                let uc = this.usercontentlist[i];

                //Check if selected video being ended is not marked completed earlier
                if (uc.contentId == this.selectedContentId  && !uc.isComplete) {

                    //make video marked completed
                    this.userContentService.updateLoggedIn(this.selectedContentId).subscribe(isMarkComplete => {
                        if (isMarkComplete) {
                            uc.isComplete = true;

                            //update progress bar
                            this.getProgress(this.usercontentlist);
                        }
                    });
                }
            }
        }
    }
    
    //get Youtube Video Id from url
    getYoutubeVideoId(url) {
        var regExp = /^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        var match = url.match(regExp);

        if (match && match[2].length == 11) {
            return match[2];
        } else {
            return 'error';
        }
    }

    //Get Logged in User's UserContent
    getLoggedinUserContent() {

        // get usercontentlist by logged userid
        this.userContentService.getListLoggedIn().subscribe(list => {

            this.usercontentlist = list;
            this.defaultusercontent = list[0];

            this.getProgress(list);
        });
    }

    //get progress of contents completed
    getProgress(list: UserContentList[]) {

        this.totalContents = list.length;
        this.contentCompletedCount = 0;
        this.contentCompletedPercent = 0;

        for (let i in list) {
            if (list[i].isComplete) {
                this.contentCompletedCount++;
            }
        }
        this.contentCompletedPercent = (this.contentCompletedCount / this.totalContents) * 100;
    }

    //when specific usercontent is clicked
    onUserContentClick(uc: UserContentList) {

        // check if youtube player API is ready and Check if same link is not clicked
        if (this.player && this.lblVideoName != uc.contentName)
        {
            this.lblVideoName = uc.contentName;

            this.videoId = this.getYoutubeVideoId(uc.contentUrl);
            this.player.cueVideoById(this.videoId); //cueVideoById doesnt play by default

            this.selectedContentId = uc.contentId;
        }
    }
}
