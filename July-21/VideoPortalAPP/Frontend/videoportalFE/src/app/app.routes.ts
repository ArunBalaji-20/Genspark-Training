import { Routes } from '@angular/router';
import { UploadVideos } from './upload-videos/upload-videos';
import { ListVideos } from './list-videos/list-videos';

export const routes: Routes = [
    {path:'upload',component:UploadVideos},
    {path:'',component:ListVideos},

];
