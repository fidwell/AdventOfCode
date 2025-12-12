import { Year } from "./year";

let yearList: HTMLDivElement;
let dayList: HTMLDivElement;
let canvas: HTMLCanvasElement;

export function initUi(years: Year[]) {
  yearList = document.getElementById("yearList") as HTMLDivElement;
  dayList = document.getElementById("dayList") as HTMLDivElement;
  canvas = document.getElementsByTagName("canvas")[0];
  
  for (let i = 0; i < years.length; i++) {
    const link = document.createElement("a") as HTMLAnchorElement;
    link.href = "";
    link.addEventListener("click", (ev: PointerEvent) => {
      ev.preventDefault();
      years[i].load();
    });
    link.innerText = `[${years[i].year}]`;
    yearList.appendChild(link);
  }
}

export function selectDay(day: number) {
  for (let i = 0; i < dayList.childNodes.length; i++) {
    const element = dayList.childNodes[i] as HTMLAnchorElement;
    element.classList.remove("selected");
    if (i === day) {
      element.classList.add("selected");
    }
  }
}

export function clearDayList(): void {
  while (dayList.firstChild) {
    dayList.removeChild(dayList.firstChild);
  }
}

export function writeToDayList(value: string): void {
  clearDayList();
  dayList.append(value);
}

export function appendToDayList(e: Element): void {
  dayList.appendChild(e);
}
