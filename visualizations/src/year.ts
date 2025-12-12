import { appendToDayList, clearDayList, selectDay } from "./ui";

export abstract class Year {
  year: number;
  days: (() => void)[];

  constructor(year: number, days: (() => void)[]) {
    this.year = year;
    this.days = days;
  }

  isImplemented(day: number): boolean {
    return this.days[day] !== null;
  }

  load(): void {
    clearDayList();
    for (let i = 0; i < this.days.length; i++) {
      const func = this.days[i];
      if (func !== null) {
        const link = document.createElement("a") as HTMLAnchorElement;
        link.href = "";
        link.addEventListener("click", (ev: PointerEvent) => {
          ev.preventDefault();
          selectDay(i);
          this.visualize(i);
        });
        link.innerText = `[${i + 1}]`;
        appendToDayList(link);
      }
    }
  }

  visualize(day: number): void {
    const func = this.days[day];
    if (func == null) {
      console.log(`No visualizer implemented for ${this.year} day ${day}.`);
    } else {
      func();
    }
  }
}
