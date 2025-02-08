w $w
set c $w.c

label $w.msg -font $font -wraplength 5i -justify left -text "This widget allows you to experiment with different widths and arrowhead shapes for lines in canvases.  To change the line width or the shape of the arrowhead, drag any of the three boxes attached to the oversized arrow.  The arrows on the right give examples at normal scale.  The text at the bottom shows the configuration options as you'd enter them for a canvas line item."
pack $w.msg -side top

## See Code / Dismiss buttons
set btns [addSeeDismiss $w.buttons $w]
pack $btns -side bottom -fill x

canvas $c -width 500 -height 350 -relief sunken -borderwidth 2
pack $c -expand yes -fill both

set demo_arrowInfo(a) 8
set demo_arrowInfo(b) 10
set demo_arrowInfo(c) 3
set demo_arrowInfo(width) 2
set demo_arrowInfo(motionProc) arrowMoveNull
set demo_arrowInfo(x1) 40
set demo_arrowInfo(x2) 350
set demo_arrowInfo(y) 150
set demo_arrowInfo(smallTips) {5 5 2}
set demo_arrowInfo(count) 0
if {[winfo depth $c] > 1} {
    set demo_arrowInfo(bigLineStyle) "-fill SkyBlue1"
    set demo_arrowInfo(boxStyle) "-fill {} -outline black -width 1"
    set demo_arrowInfo(activeStyle) "-fill red -outline black -width 1"
} else {
    # Main widget program sets variable tk_demoDirectory
    set demo_arrowInfo(bigLineStyle) "-fill black \
	-stipple @[file join $tk_demoDirectory images grey.25]"
    set demo_arrowInfo(boxStyle) "-fill {} -outline black -width 1"
    set demo_arrowInfo(activeStyle) "-fill black -outline black -width 1"
}
arrowSetup $c
$c bind box <Enter> "$c itemconfigure current $demo_arrowInfo(activeStyle)"
$c bind box <Leave> "$c itemconfigure current $demo_arrowInfo(boxStyle)"
$c bind box <B1-Enter> " "
$c bind box <B1-Leave> " "
$c bind box1 <1> {set demo_arrowInfo(motionProc) arrowMove1}
$c bind box2 <1> {set demo_arrowInfo(motionProc) arrowMove2}
$c bind box3 <1> {set demo_arrowInfo(motionProc) arrowMove3}
$c bind box <B1-Motion> "\$demo_arrowInfo(motionProc) $c %x %y"
bind $c <Any-ButtonRelease-1> "arrowSetup $c"

# arrowMove1 --
# This procedure is called for each mouse motion event on box1 (the
# one at th