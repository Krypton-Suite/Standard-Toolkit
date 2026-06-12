# Python 3.10+
# Creates a 16x16 24-bit BMP depicting a simple calculator.
# Default output: Source\Krypton Components\Krypton.Toolkit\Resources\KryptonCalcInput.bmp

from __future__ import annotations

from pathlib import Path
from typing import List, Tuple
import sys

# Image size
WIDTH = 16
HEIGHT = 16

# Colors (B, G, R) for BMP storage convenience
BGR = Tuple[int, int, int]

MAGENTA: BGR = (255, 0, 255)     # transparent key background for legacy use
GRAY: BGR = (200, 200, 200)       # body
DARK: BGR = (30, 30, 30)          # outline
DISPLAY: BGR = (170, 220, 180)    # display area
BTN: BGR = (230, 230, 230)        # buttons
BTN_EDGE: BGR = (60, 60, 60)      # button outline
EQUALS: BGR = (200, 210, 250)     # equals button


def clamp8(v: int) -> int:
    if v < 0:
        return 0
    if v > 255:
        return 255
    return v


def new_canvas(color: BGR) -> List[List[BGR]]:
    return [[color for _ in range(WIDTH)] for _ in range(HEIGHT)]


def set_px(img: List[List[BGR]], x: int, y: int, color: BGR) -> None:
    if 0 <= x < WIDTH and 0 <= y < HEIGHT:
        b, g, r = color
        img[y][x] = (clamp8(b), clamp8(g), clamp8(r))


def fill_rect(img: List[List[BGR]], x0: int, y0: int, x1: int, y1: int, color: BGR) -> None:
    if x1 < x0:
        x0, x1 = x1, x0
    if y1 < y0:
        y0, y1 = y1, y0
    for y in range(max(0, y0), min(HEIGHT - 1, y1) + 1):
        for x in range(max(0, x0), min(WIDTH - 1, x1) + 1):
            set_px(img, x, y, color)


def draw_rect(img: List[List[BGR]], x0: int, y0: int, x1: int, y1: int, color: BGR) -> None:
    if x1 < x0:
        x0, x1 = x1, x0
    if y1 < y0:
        y0, y1 = y1, y0
    for x in range(x0, x1 + 1):
        set_px(img, x, y0, color)
        set_px(img, x, y1, color)
    for y in range(y0, y1 + 1):
        set_px(img, x0, y, color)
        set_px(img, x1, y, color)


def draw_calculator() -> List[List[BGR]]:
    img = new_canvas(MAGENTA)

    # Body
    fill_rect(img, 1, 1, 14, 14, GRAY)
    draw_rect(img, 1, 1, 14, 14, DARK)

    # Display
    fill_rect(img, 3, 3, 12, 6, DISPLAY)
    draw_rect(img, 3, 3, 12, 6, DARK)

    # Buttons 2 rows of 3 + wide equals bottom
    buttons = [
        (3, 7, 5, 9), (7, 7, 9, 9), (11, 7, 13, 9),
        (3, 10, 5, 12), (7, 10, 9, 12), (11, 10, 13, 12),
    ]
    for (x0, y0, x1, y1) in buttons:
        fill_rect(img, x0, y0, x1, y1, BTN)
        draw_rect(img, x0, y0, x1, y1, BTN_EDGE)

    # Equals wide
    fill_rect(img, 3, 13, 13, 14, EQUALS)
    draw_rect(img, 3, 13, 13, 14, BTN_EDGE)

    return img


def to_24bit_bottom_up(img: List[List[BGR]]) -> bytes:
    # Each row is WIDTH*3 bytes; for width=16 -> 48 bytes (already multiple of 4, so no padding).
    out = bytearray()
    for y in range(HEIGHT - 1, -1, -1):
        for x in range(WIDTH):
            b, g, r = img[y][x]
            out.extend((b, g, r))
    return bytes(out)


def write_bmp_24(path: Path, img: List[List[BGR]]) -> None:
    pixel_data = to_24bit_bottom_up(img)
    file_header_size = 14
    info_header_size = 40
    off_bits = file_header_size + info_header_size
    size_image = len(pixel_data)
    file_size = off_bits + size_image

    # BITMAPFILEHEADER
    bfType = b"BM"
    bfSize = file_size.to_bytes(4, "little")
    bfReserved1 = (0).to_bytes(2, "little")
    bfReserved2 = (0).to_bytes(2, "little")
    bfOffBits = off_bits.to_bytes(4, "little")
    file_header = bfType + bfSize + bfReserved1 + bfReserved2 + bfOffBits

    # BITMAPINFOHEADER
    biSize = info_header_size.to_bytes(4, "little")
    biWidth = WIDTH.to_bytes(4, "little", signed=True)
    biHeight = HEIGHT.to_bytes(4, "little", signed=True)
    biPlanes = (1).to_bytes(2, "little")
    biBitCount = (24).to_bytes(2, "little")
    biCompression = (0).to_bytes(4, "little")  # BI_RGB
    biSizeImage = size_image.to_bytes(4, "little")
    biXPelsPerMeter = (0).to_bytes(4, "little")
    biYPelsPerMeter = (0).to_bytes(4, "little")
    biClrUsed = (0).to_bytes(4, "little")
    biClrImportant = (0).to_bytes(4, "little")
    info_header = (
        biSize + biWidth + biHeight + biPlanes + biBitCount + biCompression +
        biSizeImage + biXPelsPerMeter + biYPelsPerMeter + biClrUsed + biClrImportant
    )

    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("wb") as f:
        f.write(file_header)
        f.write(info_header)
        f.write(pixel_data)


def default_output_path() -> Path:
    # Scripts/make_calc_icon.py -> repo root -> Source/.../Resources/KryptonCalcInput.bmp
    repo_root = Path(__file__).resolve().parents[1]
    return repo_root / "Source" / "Krypton Components" / "Krypton.Toolkit" / "Resources" / "KryptonCalcInput.bmp"


def main(argv: list[str]) -> int:
    out_path = Path(argv[1]) if len(argv) > 1 else default_output_path()
    img = draw_calculator()
    write_bmp_24(out_path, img)
    print(f"Wrote 16x16 BMP to {out_path}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main(sys.argv))
