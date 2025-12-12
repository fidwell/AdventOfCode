import { Year } from "./year";
import { Year2025 } from "./2025/year2025";
import { initUi, writeToDayList } from "./ui";

const years: Year[] = [
  new Year2025()
]

window.addEventListener("load", async () => {
  initUi(years);
  var yearLinks = Array.from(document.getElementsByClassName("yearPicker"));
  yearLinks.forEach((yearPicker: HTMLAnchorElement) => {
    yearPicker.addEventListener("click", function (ev: PointerEvent) {
      ev.preventDefault();

      for (let i = 0; i < yearLinks.length; i++) {
        yearLinks[i].classList.remove("selected");
      }
      (ev.currentTarget as HTMLAnchorElement).classList.add("selected");

      const year = years.filter(y => y.year === Number.parseInt(yearPicker.innerText))[0];
      if (year === undefined) {
        writeToDayList(`No visualizers implemented for ${yearPicker.innerText}.`);
      } else {
        year.load();
      }
    });
  });
  years[0].load();
});
