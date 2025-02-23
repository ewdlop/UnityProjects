namespace System.Workflow.ComponentModel.Design
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    #region Glyphs


    #region Class LockedActivityGlyph
    //Class is internal but not sealed as we dont expect the ActivityDesigner writers to supply their own
    //Glyph instead based on comment property comment glyph is shown
    //Exception: StripItemCommentGlyph
    [Obsolete("The System.Workflow.* types are deprecated.  Instead, please use the new types from System.Activities.*")]
    public class LockedActivityGlyph : DesignerGlyph
    {
        private static LockedActivityGlyph defaultLockedGlyph = null;

        internal static LockedActivityGlyph Default
        {
            get
            {
                if (LockedActivityGlyph.defaultLockedGlyph == null)
                    LockedActivityGlyph.defaultLockedGlyph = new LockedActivityGlyph();
                return LockedActivityGlyph.defaultLockedGlyph;
            }
        }

        public LockedActivityGlyph()
        {
        }

        public override Rectangle GetBounds(ActivityDesigner designer, bool activated)
        {
            if (designer == null)
                throw new ArgumentNullException("designer");

            Rectangle bounds = designer.Bounds;
            bounds.Inflate(WorkflowTheme.CurrentTheme.AmbientTheme.Margin);
            return bounds;
        }

        public override int Priority
        {
            get
            {
                return DesignerGlyph.LockedGlyphPriority;
            }
        }

        protected override void OnPaint(Graphics graphics, bool activated, AmbientTheme ambientTheme, ActivityDesigner designer)
        {
            Rectangle bounds = GetBounds(designer, activated);
            bounds.Inflate(WorkflowTheme.CurrentTheme.AmbientTheme.Margin);
            ActivityDesignerPaint.DrawImage(graphics, AmbientTheme.LockImage, bounds, DesignerContentAlignment.TopLeft);
        }
    }
    #endregion

    #endr