import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../core/services/product.service';
import { CreateProductRequest, Product } from '../../../core/models/product.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit {
  product: CreateProductRequest = {
    name: '',
    price: 0,
    quantity: 0
  };

  isEditMode = false;
  productId: string | null = null;
  isLoading = false;
  isSaving = false;
  errorMessage = '';

  constructor(
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('id');

    if (this.productId) {
      this.isEditMode = true;
      this.loadProduct(this.productId);
    }
  }

  loadProduct(id: string): void {
    this.isLoading = true;

    this.productService.getProductById(id).subscribe({
      next: (product: Product) => {
        this.product = {
          name: product.name,
          price: product.price,
          quantity: product.quantity
        };
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Erro ao carregar produto';
        this.isLoading = false;
        console.error('Erro:', error);
      }
    });
  }

  onSubmit(): void {
    this.isSaving = true;
    this.errorMessage = '';

    if (this.isEditMode && this.productId) {
      this.productService.updateProduct(this.productId, this.product).subscribe({
        next: () => {
          this.router.navigate(['/products']);
        },
        error: (error) => {
          this.isSaving = false;
          this.errorMessage = error.error?.message || 'Erro ao atualizar produto';
        }
      });
    } else {
      this.productService.createProduct(this.product).subscribe({
        next: () => {
          this.router.navigate(['/products']);
        },
        error: (error) => {
          this.isSaving = false;
          this.errorMessage = error.error?.message || 'Erro ao criar produto';
        }
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }
}
