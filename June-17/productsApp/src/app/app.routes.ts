import { Routes } from '@angular/router';
import { Products } from './products/products';
import { About } from './about/about';
import { Login } from './login/login';
import { Profile } from './profile/profile';
import { authGuard } from './auth-guard';
import { SingleProduct } from './single-product/single-product';

export const routes: Routes = [
    {path:'home',component:Products,canActivate:[authGuard],children:
        [
            {path:'product/:n',component:SingleProduct}
        ]
    },
    {path:'about',component:About},
    {path:'login',component:Login},
    {path:'profile',component:Profile,canActivate:[authGuard]},
    // {path:'product/:n',component:SingleProduct,canActivate:[authGuard]}
];
