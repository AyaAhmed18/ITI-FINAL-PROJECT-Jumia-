import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ReloadService {

  private reloadCount: number = 0;

  constructor() { }

  incrementReloadCount() {
    this.reloadCount++;
  }

  getReloadCount() {
    return this.reloadCount;
  }

  resetReloadCount() {
    this.reloadCount = 0;
  }
}
