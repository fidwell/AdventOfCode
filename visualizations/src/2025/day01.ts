let ctx: CanvasRenderingContext2D;

const canvasSize = 500;
let pointAt = 50;
let zeroes = 0;

export function Visualize_2025_01() {
  const canvas = document.getElementById("canvas") as HTMLCanvasElement;
  canvas.style.setProperty("width", `${canvasSize}px`);
  canvas.style.setProperty("height", `${canvasSize}px`);
  ctx = canvas.getContext("2d");
  ctx.canvas.width = canvasSize;
  ctx.canvas.height = canvasSize;

  window.requestAnimationFrame(draw);
}

function draw(time: number) {
  pointAt = (pointAt + 1) % 100; // tick up
  if (pointAt === 0)
    zeroes++;

  ctx.clearRect(0, 0, canvasSize, canvasSize); // clear canvas

  const cX = canvasSize / 2;
  const cY = canvasSize / 2;

  // Draw dial
  const cR = (canvasSize / 2) - 10;
  ctx.beginPath();
  ctx.arc(cX, cY, cR, 0, 360);
  ctx.strokeStyle = "rgb(255 255 255)";
  ctx.lineWidth = 3;
  ctx.closePath();
  ctx.stroke();

  // Draw pointer
  const pR = (canvasSize / 2) - 20;
  const angle = (pointAt / 100) * 2 * Math.PI + (Math.PI); //(time % 360) * 0.0174533; // in radians
  ctx.beginPath();
  ctx.moveTo(cX, cY);
  let pX = 2 * cX - (cX + Math.sin(angle) * pR);
  let pY = cY + Math.cos(angle) * pR;
  ctx.lineTo(pX, pY);
  ctx.stroke();

  // Write value
  ctx.font = "24px monospace";
  ctx.fillStyle = "white";
  ctx.fillText(`${pointAt}`, 10, 30);
  ctx.fillText(`${zeroes}`, 10, 50);

  window.requestAnimationFrame(draw);
}
