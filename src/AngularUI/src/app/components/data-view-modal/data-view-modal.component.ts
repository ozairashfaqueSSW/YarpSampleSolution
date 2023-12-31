import { ChangeDetectionStrategy, Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { DataModel } from '../../models/DataModel';

@Component({
  selector: 'app-data-view-modal',
  templateUrl: './data-view-modal.component.html',
  styleUrl: './data-view-modal.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataViewModalComponent {
  @Input() dataModel: DataModel = {source:"", data:""};
  @Input() selectedEndpoint: string = "";
  @Input() fullPath: string = "";
  @Output() closeModalEvent = new EventEmitter<void>();

  @HostListener('document:click', ['$event'])
  onClickOutsideModal(event: Event) {
    if (!(event.target as HTMLElement).closest('.modal')) {
      this.closeModal();
    }
  }

  @HostListener('document:keydown.escape', ['$event'])
  onEscapeKey(event: KeyboardEvent) {
    this.closeModal();
  }

  closeModal() {
    this.closeModalEvent.emit();
  }
}
