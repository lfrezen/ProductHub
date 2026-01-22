import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login.component')
      .then(m => m.LoginComponent),
    data: { showNavbar: false }
  },
  {
    path: 'products',
    canActivate: [authGuard],  // ← Guard protege TODAS as rotas filhas
    children: [
      {
        path: '',  // /products
        loadComponent: () => import('./features/products/product-list/product-list.component')
          .then(m => m.ProductListComponent),
        data: { showNavbar: true }
      },
      {
        path: 'new',  // /products/new
        loadComponent: () => import('./features/products/product-form/product-form.component')
          .then(m => m.ProductFormComponent),
        data: { showNavbar: true }
      },
      {
        path: 'edit/:id',  // /products/edit/:id
        loadComponent: () => import('./features/products/product-form/product-form.component')
          .then(m => m.ProductFormComponent),
        data: { showNavbar: true }
      }
    ]
  },
  {
    path: '',
    redirectTo: '/products',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: '/products'
  }
];
