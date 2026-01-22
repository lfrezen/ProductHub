import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { Product, RegisterSaleRequest } from '../../../core/models/product.model';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CurrencyPipe, DatePipe, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  isLoading = true;
  errorMessage = '';

  selectedProduct: Product | null = null;
  saleQuantity: number = 1;
  isRegistering = false;
  saleErrorMessage = '';

  constructor(
    private productService: ProductService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.productService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Erro ao carregar produtos';
        this.isLoading = false;
        console.error('Erro:', error);
      }
    });
  }

  deleteProduct(id: string, name: string): void {
    if (confirm(`Deseja realmente excluir o produto "${name}"?`)) {
      this.productService.deleteProduct(id).subscribe({
        next: () => {
          this.products = this.products.filter(p => p.id !== id);
        },
        error: (error) => {
          alert('Erro ao excluir produto');
          console.error('Erro:', error);
        }
      });
    }
  }

  navigateToCreate(): void {
    this.router.navigate(['/products/new']);
  }

  navigateToEdit(id: string): void {
    this.router.navigate(['/products/edit', id]);
  }

  openSaleModal(product: Product): void {
    this.selectedProduct = product;
    this.saleQuantity = 1;
    this.saleErrorMessage = '';
  }

  closeSaleModal(): void {
    this.selectedProduct = null;
    this.saleQuantity = 1;
    this.saleErrorMessage = '';
  }

  registerSale(): void {
    if (!this.selectedProduct) return;

    if (this.saleQuantity <= 0) {
      this.saleErrorMessage = 'Quantidade deve ser maior que zero';
      return;
    }

    if (this.saleQuantity > this.selectedProduct.quantity) {
      this.saleErrorMessage = `Estoque insuficiente. Disponível: ${this.selectedProduct.quantity}`;
      return;
    }

    this.isRegistering = true;
    this.saleErrorMessage = '';

    const request: RegisterSaleRequest = {
      quantity: this.saleQuantity
    };

    this.productService.registerSale(this.selectedProduct.id, request).subscribe({
      next: () => {
        const index = this.products.findIndex(p => p.id === this.selectedProduct!.id);
        if (index !== -1) {
          this.products[index].quantity -= this.saleQuantity;
          this.products[index].lastSaleDate = new Date().toISOString();

          if (this.products[index].quantity === 0) {
            this.products[index].isOutOfStock = true;
          }
        }

        this.isRegistering = false;
        this.closeSaleModal();
      },
      error: (error) => {
        this.isRegistering = false;
        this.saleErrorMessage = error.error?.message || 'Erro ao registrar venda';
      }
    });
  }
}
