using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Bloq
{
    public partial class Form1 : Form
    {
        public Bitmap drawArea;
        public Pen pen, penGreen, penRed;
        public Pen dashedBlack, dashedGreen, dashedRed;
        private List<Figure> figures;
        private bool IsStart = false;
        private bool MoveInAction = false;
        private const int ElipseX = 200;
        private const int ElipseY = 100;
        private Figure selected = null;
        const int radius = 10; // 2 x radius of small circles
        bool adding = false;
        ARROW addingArrow = null;

        public Form1()
        {
            InitializeComponent();
            figures = new List<Figure>();
            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
            }
            pen = new Pen(Brushes.Black, 3);
            penGreen = new Pen(Brushes.LightGreen, 3);
            penRed = new Pen(Brushes.Red, 3);

            dashedBlack = new Pen(Brushes.Black, 3);
            dashedBlack.DashPattern = new float[] { 2, 1 };
            dashedGreen = new Pen(Brushes.LightGreen, 3);
            dashedGreen.DashPattern = new float[] { 2, 1 };
            dashedRed = new Pen(Brushes.Red, 3);
            dashedRed.DashPattern = new float[] { 2, 1 };
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Clicked_to_draw(object sender, MouseEventArgs e)
        {
            adding = false;
            if (e.Button == MouseButtons.Left)
            {
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                System.Drawing.SolidBrush drawBrushBack = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                Figure f = new Figure();

                if (OPbutton.Checked)
                {
                    //s = "Blok operacyjny";
                    Point[] p = { new Point(e.X - 100, e.Y + 50), new Point(e.X - 100, e.Y - 50), new Point(e.X + 100, e.Y - 50), new Point(e.X + 100, e.Y + 50) };
                    f = new OPblock(p);
                    figures.Add(f);
                }
                if (DECbutton.Checked)
                {

                    Point[] p = { new Point(e.X - 100, e.Y), new Point(e.X, e.Y - 50), new Point(e.X + 100, e.Y), new Point(e.X, e.Y + 50) };
                    f = new DECblock(p);
                    figures.Add(f);
                }
                if (STARTbutton.Checked)
                {
                    if (IsStart)
                    {
                        MessageBox.Show("Blok startowy już istnieje");
                        return;
                    }

                    Point[] p = { new Point(e.X, e.Y) };
                    f = new STARTblock(p);
                    figures.Add(f);
                    IsStart = true;
                }
                if (ENDbutton.Checked)
                {

                    Point[] p = { new Point(e.X, e.Y) };
                    f = new ENDblock(p);
                    figures.Add(f);
                }
                if (DELETEbutton.Checked)
                {
                    (Figure delF, int index) = SearchFromClick(e);
                    if (index == figures.Count || index==-1) return;
                    if (delF._type == Types.START) IsStart = false;
                    if (index >= 0 && index < figures.Count)
                    {
                        //Canvas.Refresh();
                        figures.Remove(delF);
                        if (selected == delF)
                        {
                            textBox.Enabled = true;
                            textBox.Text = "";
                            selected = null;
                        }
                        RemoveArrowsWithIndex(index);
                        DrawFromIndex(figures, 0);
                        Canvas.Refresh();
                    }

                }
                if(LINK_button.Checked)
                {
                    
                   (int index, bool GonnaAdd,int socketId) = add_arrow(true, e);
                   if (GonnaAdd)
                    {
                        adding = true;
                        addingArrow = new ARROW(new Point(e.X, e.Y), new Point(e.X, e.Y));
                        addingArrow.indexFrom = index;
                        figures.Add(addingArrow);
                    }
                    else
                    {
                        if(index>0 && index<figures.Count) figures[index].setArrow(socketId, false);
                        
                    }
                }
                DrawFromIndex(figures, 0);
            }
            if (e.Button == MouseButtons.Right)
            {
                (Figure f, int index) = SearchFromClick(e);
                if (f != null)
                {
                    if (selected == f) return;
                    selected = f;
                    if (selected._type == Types.START || selected._type == Types.END)
                    {
                        textBox.Enabled = false;
                        textBox.Text = selected._text;
                    }
                    else
                    {
                        textBox.Enabled = true;
                        textBox.Text = selected._text;
                    }
                }
                else
                {
                    selected = null;
                    textBox.Enabled = true;
                    textBox.Text = "";
                }
                DrawFromIndex(figures, 0);
                Canvas.Refresh();
            }
            if (e.Button == MouseButtons.Middle && selected != null)
            {
                MoveInAction = true;
            }
        }


        enum Types { OP = 0, DEC = 1, START = 2, END = 3, LINK = 4, DELETE = 5, ARROW=6 ,NULLFIG = 7 };

        private class Figure
        {
            public Types _type;
            public Point[] _points;
            public string _text;
            const int radius = 10;

            public Figure() { _type = Types.NULLFIG; }
            public virtual bool isIn(MouseEventArgs e)
            {
                return false;
            }
            public virtual bool canInsertArrow(int socketType) { return false; }
            public virtual void setArrow(int socketType,bool setTo) { }

            public virtual (int from, int to) getIndexe() { return (-1, -1); }
        }

        private class DECblock : Figure
        {
            public bool inArrow = false;
            public bool trueArrow = false;
            public bool falseArrow = false;
            public DECblock(Point[] points) : base()
            {
                _type = Types.DEC;
                _text = "Blok decyzyjny";
                _points = points;
            }
            public DECblock(Point[] points, string text) : base()
            {
                _type = Types.DEC;
                _text = text;
                _points = points;
            }
            public DECblock(Point[] points, string text,bool IN,bool t,bool f) : base()
            {
                _type = Types.DEC;
                _text = text;
                _points = points;
                inArrow = IN;
                trueArrow = t;
                falseArrow = f;
            }
            public override bool isIn(MouseEventArgs e)
            {
                if (check(_points[0].X, _points[0].Y, _points[1].X, _points[1].Y, _points[2].X, _points[2].Y, _points[3].X, _points[3].Y, e.X, e.Y)) return true;
                return false;
            }
            public override bool canInsertArrow(int socketType)
            {
                if (socketType == 0) return !inArrow;
                if (socketType == 1) return !trueArrow;
                else return !falseArrow;
            }
            public override void setArrow(int socketType,bool setTo)
            {
                if (socketType == 0) inArrow = setTo;
                if (socketType == 1) trueArrow = setTo;
                if (socketType == 2) falseArrow = setTo;
            }

        }

        private class OPblock : Figure
        {
            public bool inArrow = false;
            public bool outArrow = false;
            public OPblock(Point[] points) : base()
            {
                _type = Types.OP;
                _text = "Blok operacyjny";
                _points = points;
            }
            public OPblock(Point[] points, string text) : base()
            {
                _type = Types.OP;
                _text = text;
                _points = points;
            }
            public OPblock(Point[] points, string text,bool IN,bool OUT) : base()
            {
                _type = Types.OP;
                _text = text;
                _points = points;
                inArrow = IN;
                outArrow = OUT;
            }
            public override bool isIn(MouseEventArgs e)
            {
                if ((_points[2].X - 200 <= e.X && _points[2].Y + 100 >= e.Y) && (_points[2].X >= e.X && _points[2].Y <= e.Y)) return true;
                return false;
            }

            public override bool canInsertArrow(int socketType)
            {
                if (socketType == 0) return !inArrow;
                return !outArrow;
            }

            public override void setArrow(int socketType, bool setTo)
            {
                if (socketType == 0) inArrow = setTo;
                if (socketType == 1) outArrow = setTo;
            }
        }

        private class STARTblock : Figure
        {
            public bool outArrow = false;
            public STARTblock(Point[] points) : base()
            {
                _type = Types.START;
                _text = "START";
                _points = points;
            }
            public STARTblock(Point[] points,bool OUT) : base()
            {
                _type = Types.START;
                _text = "START";
                _points = points;
                outArrow = OUT;
            }
            public override bool isIn(MouseEventArgs e)
            {
                if (checkpoint(_points[0].X, -_points[0].Y, e.X, -e.Y, ElipseX / 2, ElipseY / 2) <= 1) return true;
                return false;
            }
            public override bool canInsertArrow(int socketType)
            {
                return !outArrow;
            }
            public override void setArrow(int socketType, bool setTo)
            {
                outArrow = setTo;
            }
        }

        private class ENDblock : Figure
        {
            public bool inArrow = false;
            public ENDblock(Point[] points) : base()
            {
                _type = Types.END;
                _text = "STOP";
                _points = points;
            }
            public ENDblock(Point[] points,bool IN) : base()
            {
                _type = Types.END;
                _text = "STOP";
                _points = points;
                inArrow = IN;
            }

            public override bool isIn(MouseEventArgs e)
            {
                if (checkpoint(_points[0].X, -_points[0].Y, e.X, -e.Y, ElipseX / 2, ElipseY / 2) <= 1) return true;
                return false;
            }
            public override bool canInsertArrow(int socketType)
            {
                return !inArrow;
            }
            public override void setArrow(int socketType, bool setTo)
            {
                inArrow = setTo;
            }
        }

        private class ARROW:Figure
        {
            public int indexFrom = -1;
            public int indexTo=-1;
            public ARROW(Point begin):base()
            {
                _type = Types.ARROW;
                _points = new Point[2];
                _points[0] = begin;
            }
            public ARROW(Point begin,Point end):base()
            {
                _type = Types.ARROW;
                _points = new Point[2];
                _points[0] = begin;
                _points[1] = end;
            }

            public override (int from, int to) getIndexe()
            {
                return (indexFrom, indexTo);
            }
        }

        private void NewScheme(object sender, EventArgs e)
        {
            Form2 PopUp = new Form2(this);
            if (PopUp.ShowDialog() == DialogResult.OK)
            {
                int h = PopUp.GetHight;
                int w = PopUp.GetWidth;
                selected = null;
                Size S = new Size(w, h);
                drawArea = new Bitmap(S.Width, S.Height);
                Canvas.Image = drawArea;
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    g.Clear(Color.White);
                }
                Canvas.Refresh();
                figures.Clear();
                IsStart = false;

            }
        }

        private void DrawFigure(Figure f, Pen PEN)
        {
            if (f == null) return;
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.SolidBrush drawBrushBack = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            StringFormat stringFormat = new StringFormat(StringFormatFlags.NoClip);
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            string str = f._text;
            if (f._type == Types.DEC || f._type == Types.OP)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    g.FillPolygon(drawBrushBack, f._points);
                    g.DrawPolygon(PEN, f._points);
                    RectangleF rectangleF;
                    if (f._type == Types.OP)
                    {
                        rectangleF = new RectangleF(f._points[1].X, f._points[1].Y, 200, 100);
                        g.DrawEllipse(pen, f._points[1].X + 100 - radius / 2, f._points[1].Y - radius, radius, radius);
                        g.FillEllipse(drawBrushBack, f._points[1].X + 100 - radius / 2, f._points[1].Y - radius, radius, radius);

                        g.DrawEllipse(pen, f._points[0].X + 100 - radius / 2, f._points[0].Y, radius, radius);
                        g.FillEllipse(drawBrush, f._points[0].X + 100 - radius / 2, f._points[0].Y, radius, radius);
                        g.DrawString(str, drawFont, drawBrush, rectangleF, stringFormat);

                    }
                    if (f._type == Types.DEC)
                    {
                        rectangleF = new RectangleF(f._points[1].X - 50, f._points[1].Y + 25, 100, 50);
                        g.DrawEllipse(pen, f._points[1].X - radius / 2, f._points[1].Y - radius, radius, radius);
                        g.FillEllipse(drawBrushBack, f._points[1].X - radius / 2, f._points[1].Y - radius, radius, radius);

                        g.DrawString("T", drawFont, drawBrush, f._points[0].X-radius, f._points[0].Y-2*radius-5);
                        g.DrawEllipse(pen, f._points[0].X - radius, f._points[0].Y - radius / 2, radius, radius);
                        g.FillEllipse(drawBrush, f._points[0].X - radius, f._points[0].Y - radius / 2, radius, radius);

                        g.DrawString("F", drawFont, drawBrush, f._points[2].X, f._points[2].Y - 2*radius-5);
                        g.DrawEllipse(pen, f._points[2].X, f._points[2].Y - radius / 2, radius, radius);
                        g.FillEllipse(drawBrush, f._points[2].X, f._points[2].Y - radius / 2, radius, radius);
                        g.DrawString(str, drawFont, drawBrush, rectangleF, stringFormat);
                    }
                }

            }
            if (f._type == Types.START)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    RectangleF rectangleF = new RectangleF(f._points[0].X - 30, f._points[0].Y - 25, 70, 50);
                    g.FillEllipse(drawBrushBack, f._points[0].X - ElipseX / 2, f._points[0].Y - ElipseY / 2, ElipseX, ElipseY);
                    g.DrawEllipse(PEN, f._points[0].X - ElipseX / 2, f._points[0].Y - ElipseY / 2, ElipseX, ElipseY);
                    g.DrawString(str, drawFont, drawBrush, rectangleF, stringFormat);

                    g.DrawEllipse(pen, f._points[0].X-radius/2, f._points[0].Y + ElipseY / 2, radius, radius);
                    g.FillEllipse(drawBrush, f._points[0].X-radius/2, f._points[0].Y + ElipseY / 2, radius, radius);
                }
            }
            if (f._type == Types.END)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    RectangleF rectangleF = new RectangleF(f._points[0].X - 30, f._points[0].Y - 25, 70, 50);
                    g.FillEllipse(drawBrushBack, f._points[0].X - ElipseX / 2, f._points[0].Y - ElipseY / 2, ElipseX, ElipseY);
                    g.DrawEllipse(PEN, f._points[0].X - ElipseX / 2, f._points[0].Y - ElipseY / 2, ElipseX, ElipseY);
                    g.DrawString(str, drawFont, drawBrush, rectangleF, stringFormat);

                    g.DrawEllipse(pen, f._points[0].X-radius/2, f._points[0].Y - ElipseY / 2 - radius, radius, radius);
                    g.FillEllipse(drawBrushBack, f._points[0].X-radius/2, f._points[0].Y - ElipseY / 2 - radius, radius, radius);
                }
            }
            if (f._type == Types.ARROW) 
                DrawArrow(f._points[0], f._points[1]);
            drawFont.Dispose();
            drawBrush.Dispose();
            drawBrushBack.Dispose();
        }

        private void DrawFromIndex(List<Figure> fList, int index)
        {
            if (index < 0 || index > fList.Count) return;
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
            }
            //Canvas.Refresh();
            for (int i = index; i < fList.Count; i++)
            {
                if (fList[i]._type == Types.DEC || fList[i]._type == Types.OP) DrawFigure(fList[i], pen);
                if (fList[i]._type == Types.START) DrawFigure(fList[i], penGreen);
                if (fList[i]._type == Types.END) DrawFigure(fList[i], penRed);
                if (fList[i]._type == Types.ARROW) DrawFigure(fList[i], pen);
                if (fList[i] == selected)
                {
                    if (fList[i]._type == Types.DEC || fList[i]._type == Types.OP) DrawFigure(fList[i], dashedBlack);
                    if (fList[i]._type == Types.START) DrawFigure(fList[i], dashedGreen);
                    if (fList[i]._type == Types.END) DrawFigure(fList[i], dashedRed);
                }
            }
            Canvas.Refresh();
        }

        private void Change_text(object sender, EventArgs e)
        {
            if (selected != null)
            {
                int index = figures.IndexOf(selected);
                selected._text = textBox.Text;
                DrawFromIndex(figures, 0);
            }
        }

        private void Mouse_move_event(object sender, MouseEventArgs e)
        {
            if (selected != null && MoveInAction && e.Button == MouseButtons.Middle)
            {
                Figure f;
                switch (selected._type)
                {
                    case Types.DEC:
                        {
                            Point[] p = { new Point(e.X - 100, e.Y), new Point(e.X, e.Y - 50), new Point(e.X + 100, e.Y), new Point(e.X, e.Y + 50) };
                            f = new DECblock(p, selected._text);
                            break;
                        }
                    case Types.OP:
                        {
                            Point[] p = { new Point(e.X - 100, e.Y + 50), new Point(e.X - 100, e.Y - 50), new Point(e.X + 100, e.Y - 50), new Point(e.X + 100, e.Y + 50) };
                            f = new OPblock(p, selected._text);
                            break;
                        }
                    case Types.START:
                        {
                            Point[] p = { new Point(e.X, e.Y) };
                            f = new STARTblock(p);
                            break;
                        }
                    case Types.END:
                        {
                            Point[] p = { new Point(e.X, e.Y) };
                            f = new ENDblock(p);
                            break;
                        }
                    default:
                        f = new Figure();
                        break;
                }
                figures.Remove(selected);
                selected = f;
                figures.Add(selected);
                DrawFromIndex(figures, 0);
            }
            if(adding)
            {
                figures[figures.Count - 1]._points[1] = new Point(e.X, e.Y);
                DrawFromIndex(figures, 0);
            }
        }

        private void Mouse_up_event(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && MoveInAction)
            {
                MoveInAction = false;
            }
            if(adding)
            {
                
                (int index, bool GonnaAdd,int socketId) = add_arrow(false, e);
                if(GonnaAdd)
                {
                    
                    //addingArrow.indexTo = index;
                    //figures.Add(addingArrow);
                    addingArrow._points[1] = new Point(e.X, e.Y);
                    addingArrow.indexTo = index;
                    figures[figures.Count - 1] = addingArrow;
                    //DrawArrow(addingArrow._points[0], addingArrow._points[1]);
                    DrawFromIndex(figures, 0);
                    //MessageBox.Show("drawing + mouse_up");
                    adding = false;
                    //addingArrow = null;
                }
                else
                {
                    addingArrow = null;
                    if(index>0 && index<figures.Count) figures[index].setArrow(socketId, false);
                    figures.RemoveAt(figures.Count - 1);
                    adding = false;
                    DrawFromIndex(figures, 0);
                }
            }
        }

        private (Figure, int index) SearchFromClick(MouseEventArgs e)
        {
            //Point p = new Point(e.X, e.Y);
            for (int i = figures.Count - 1; i >= 0; i--)
            {
                if (figures[i].isIn(e))
                {
                    return (figures[i], i);
                }
            }
            return (null, figures.Count);
        }

        private (int index, bool canAdd,int socketId) add_arrow(bool begining,MouseEventArgs e)
        {
            int index = figures.Count;
            for(int i=0;i<figures.Count;i++)
            {
                switch (figures[i]._type)
                {
                    case Types.OP:
                        {
                         
                            

                            if(begining)
                            {
                                int x= figures[i]._points[0].X + 100 - radius / 2;
                                int y= figures[i]._points[0].Y;
                                // on bottom of block
                                if (e.X-x<=radius &&  e.Y-y <=radius && e.X-x>=0 && e.Y-y>=0)  
                                {
                                    
                                    if (figures[i].canInsertArrow(0))
                                    {
                                        figures[i].setArrow(0, true);
                                        
                                        return (i, true,0);
                                    }
                                    else return (i, false,0);
                                    
                                }
                                break;
                            }
                            if(!begining)
                            {
                                int x = figures[i]._points[1].X  +100 - radius / 2;
                                int y = figures[i]._points[1].Y-radius;
                                //on top of block
                                if (e.X - x <= radius
                                    && e.Y - y  <= radius
                                    && e.X - x >= 0
                                    && e.Y -y >= 0)
                                {
                                    
                                    if (figures[i].canInsertArrow(1))
                                    {
                                        
                                        figures[i].setArrow(1, true);
                                        return (i, true,1);
                                    }
                                    else return (i, false,1);
                                }
                            }
                            break;
                        }
                    case Types.DEC:
                        {
                            if(begining)
                            {
                                int x = figures[i]._points[0].X - radius;
                                int y = figures[i]._points[0].Y - radius/2;
                                if (e.X-x<=radius
                                    && e.Y-y<=radius
                                    && e.X - x >=0 
                                    && e.Y - y >= 0)
                                {
                                    if (figures[i].canInsertArrow(1))
                                    {
                                        figures[i].setArrow(1, true);
                                        return (i, true,1);
                                    }
                                    else return (i, false,1);

                                }

                                x = figures[i]._points[2].X;
                                y = figures[i]._points[2].Y - radius / 2;

                                if (e.X-x<=radius
                                    &&e.Y- y<=radius
                                    && e.X - x >=0
                                    && e.Y - y >=0)
                                {
                                    if (figures[i].canInsertArrow(2))
                                    {
                                        figures[i].setArrow(2, true);
                                        return (i, true,2);
                                    }
                                    else return (i, false,2);
                                }

                            }
                            if(!begining)
                            {
                                int x = figures[i]._points[1].X - radius / 2;
                                int y = figures[i]._points[1].Y - radius;
                                if (e.X-x<=radius
                                    &&e.Y- y<=radius
                                    && e.X - x >=0
                                    && e.Y - y >=0
                                    )
                                    {
                                    if (figures[i].canInsertArrow(0))
                                    {
                                        figures[i].setArrow(0, true);
                                        return (i, true,0);
                                    }
                                    else return (i, false,0);
                                    }
                            }
                            break;
                        }
                    case Types.START:
                        {
                            if(begining)
                            {
                                int x = figures[i]._points[0].X - radius / 2;
                                int y = figures[i]._points[0].Y + ElipseY / 2;
                                if (e.X-x<=radius
                                    &&e.Y- y<=radius
                                    && e.X - x >=0
                                    && e.Y - y >0)
                                {
                                    if (figures[i].canInsertArrow(0))
                                    {
                                        figures[i].setArrow(0, true);
                                        return (i, true,0);
                                    }
                                    else return (i, false,0);
                                }
                            }
                            break;
                        }
                    case Types.END:
                        {
                            if(!begining)
                            {
                                int x = figures[i]._points[0].X - radius / 2;
                                int y = figures[i]._points[0].Y - ElipseY / 2 - radius;
                                if (e.X-x<=radius
                                    && e.Y-y <=radius
                                    && e.X - x>=0
                                    && e.Y - y>=0
                                    )
                                    {
                                    if (figures[i].canInsertArrow(1))
                                    {
                                        figures[i].setArrow(1, true);
                                        return (i, true,0);
                                    }
                                    else return (i, false,0);
                                    }
                            }
                            break;
                        }
                }

            }

            return (index, false,-1);
        }

        private void DrawArrow(Point begin,Point end)
        {
            
            using (Graphics g= Graphics.FromImage(drawArea))
            {
                Pen p = new Pen(Color.Black, 8);
                p.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                g.DrawLine(p, end, begin);
            }
            Canvas.Invalidate();
        }

        private void SaveAS(object sender, MouseEventArgs e)
        {
            var window = new SaveFileDialog();
            window.AddExtension = true;
            window.Title = "Zapis pliku";
            window.Filter = "Bloq File format|*.bq";
            if(window.ShowDialog()==DialogResult.OK)
            {
                string s= window.FileName;
                using StreamWriter file = new(s);
                file.WriteLine(Canvas.Height);
                file.WriteLine(Canvas.Width);
                file.WriteLine(IsStart);
                file.WriteLine(figures.Count);
                for(int i=0;i<figures.Count;i++)
                {
                    file.WriteLine(figures[i]._type);
                    switch (figures[i]._type)
                    {
                        case Types.OP:
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    file.WriteLine(figures[i]._points[j].X);
                                    file.WriteLine(figures[i]._points[j].Y);
                                }
                                file.WriteLine(figures[i]._text);
                                break;
                            }
                        case Types.DEC:
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    file.WriteLine(figures[i]._points[j].X);
                                    file.WriteLine(figures[i]._points[j].Y);
                                }
                                file.WriteLine(figures[i]._text);
                                break;
                            }
                        case Types.START:
                            {
                                file.WriteLine(figures[i]._points[0].X);
                                file.WriteLine(figures[i]._points[0].Y);
                                file.WriteLine(figures[i]._text);
                                break;
                            }
                        case Types.END:
                            {
                                file.WriteLine(figures[i]._points[0].X);
                                file.WriteLine(figures[i]._points[0].Y);
                                file.WriteLine(figures[i]._text);
                                break;
                            }
                        case Types.ARROW:
                            {
                                file.WriteLine(figures[i]._points[0].X);
                                file.WriteLine(figures[i]._points[0].Y);
                                file.WriteLine(figures[i]._points[1].X);
                                file.WriteLine(figures[i]._points[1].Y);
                                (int f, int t) = figures[i].getIndexe();
                                file.WriteLine(f);
                                file.WriteLine(t);
                                break;
                            }

                    }

                }
                file.Close();
            }
            
        }

        private void LOADfrom(object sender, MouseEventArgs e)
        {
            
            var window = new OpenFileDialog();
            window.Filter = "Bloq File Format|.*bq";
            window.Title = "Wczytywanie";
            if(window.ShowDialog()==DialogResult.OK)
            {
                if (!window.CheckFileExists || !window.CheckPathExists) return;
                int h, w,fCount;
                string text;
                string s = window.FileName;
                using StreamReader file = new(s);
                text=file.ReadLine();
                h = int.Parse(text);
                text = file.ReadLine();
                w = int.Parse(text);
                text = file.ReadLine();
                IsStart = bool.Parse(text);
                text = file.ReadLine();
                fCount = int.Parse(text);
                drawArea = new Bitmap(w, h);
                Canvas.Image = drawArea;
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    g.Clear(Color.White);
                }
                Canvas.Refresh();
                figures.Clear();
                Types typ=Types.NULLFIG;
                
                for (int i=0;i<fCount;i++)
                {
                    Point[] p = new Point[4];
                    int x, y;
                    text = file.ReadLine();
                    switch (text)
                    {
                        case "OP":
                            typ = Types.OP;
                            break;
                        case "DEC":
                            typ = Types.DEC;
                            break;
                        case "START":
                            typ = Types.START;
                            break;
                        case "END":
                            typ = Types.END;
                            break;
                        case "ARROW":
                            typ = Types.ARROW;
                            break;
                    }

                    switch (typ)
                    {
                        
                        case Types.OP:
                            
                            
                            for(int j=0;j<4;j++)
                            {
                                text = file.ReadLine();
                                x = int.Parse(text);
                                text = file.ReadLine();
                                y = int.Parse(text);
                                p[j] = new Point(x, y);
                            }
                            OPblock OP = new OPblock(p);
                            text = file.ReadLine();
                            OP._text = text;
                            figures.Add(OP);
                            break;
                        case Types.DEC:
                            p = new Point[4];
                            
                            for (int j = 0; j < 4; j++)
                            {
                                text = file.ReadLine();
                                x = int.Parse(text);
                                text = file.ReadLine();
                                y = int.Parse(text);
                                p[j] = new Point(x, y);
                            }
                            DECblock DEC = new DECblock(p);
                            text = file.ReadLine();
                            DEC._text = text;
                            figures.Add(DEC);
                            break;
                        case Types.START:
                            p = new Point[1];
                            text = file.ReadLine();
                            x = int.Parse(text);
                            text = file.ReadLine();
                            y = int.Parse(text);
                            p[0] = new Point(x, y);
                            text = file.ReadLine();
                            STARTblock START = new STARTblock(p);
                            figures.Add(START);
                            break;
                        case Types.END:
                            p = new Point[1];
                            text = file.ReadLine();
                            x = int.Parse(text);
                            text = file.ReadLine();
                            y = int.Parse(text);
                            p[0] = new Point(x, y);
                            text = file.ReadLine();
                            ENDblock END = new ENDblock(p);
                            figures.Add(END);
                            break;
                        case Types.ARROW:
                            p = new Point[2];
                            int f, t;
                            text = file.ReadLine();
                            x = int.Parse(text);
                            text = file.ReadLine();
                            y = int.Parse(text);
                            p[0] = new Point(x, y);
                            text = file.ReadLine();
                            x = int.Parse(text);
                            text = file.ReadLine();
                            y = int.Parse(text);
                            p[1] = new Point(x, y);
                            text = file.ReadLine();
                            f = int.Parse(text);
                            text = file.ReadLine();
                            t = int.Parse(text);
                            ARROW arrow = new ARROW(p[0], p[1]);
                            arrow.indexFrom = f;
                            arrow.indexTo = t;
                            figures.Add(arrow);
                            break;
                    }


                }

                file.Close();
                DrawFromIndex(figures, 0);
            }
        }

        private void RemoveArrowsWithIndex(int index)
        {
            for(int i=0;i<figures.Count;i++)
            {
                (int f, int t) = figures[i].getIndexe();
                if(f==index || t==index)
                {
                    figures.RemoveAt(i);
                    
                }

            }
        }



        //https://www.geeksforgeeks.org/check-if-a-point-is-inside-outside-or-on-the-ellipse/
        private static int checkpoint(int h, int k, int x, int y, int a, int b)
        {
            int p = ((int)Math.Pow((x - h), 2) /
            (int)Math.Pow(a, 2)) +
           ((int)Math.Pow((y - k), 2) /
            (int)Math.Pow(b, 2));
            return p;
        }
        //https://www.geeksforgeeks.org/check-whether-given-point-lies-inside-rectangle-not/?fbclid=IwAR0Rk0nKZlS50AoebnzrnsLL0z2xCECO2gxTvibf2aBFdG4hQCq534K4btI
        private static float area(int x1, int y1, int x2,
                      int y2, int x3, int y3)
        {
            return (float)Math.Abs((x1 * (y2 - y3) +
                                   x2 * (y3 - y1) +
                                   x3 * (y1 - y2)) / 2.0);
        }
        //https://www.geeksforgeeks.org/check-whether-given-point-lies-inside-rectangle-not/?fbclid=IwAR0Rk0nKZlS50AoebnzrnsLL0z2xCECO2gxTvibf2aBFdG4hQCq534K4btI
        private static bool check(int x1, int y1, int x2,
                      int y2, int x3, int y3,
                   int x4, int y4, int x, int y)
        {

            // Calculate area of rectangle ABCD 
            float A = area(x1, y1, x2, y2, x3, y3) +
                      area(x1, y1, x4, y4, x3, y3);

            // Calculate area of triangle PAB 
            float A1 = area(x, y, x1, y1, x2, y2);

            // Calculate area of triangle PBC 
            float A2 = area(x, y, x2, y2, x3, y3);

            // Calculate area of triangle PCD 
            float A3 = area(x, y, x3, y3, x4, y4);

            // Calculate area of triangle PAD
            float A4 = area(x, y, x1, y1, x4, y4);

            // Check if sum of A1, A2, A3  
            // and A4is same as A 
            return (A == A1 + A2 + A3 + A4);
        }

        

    }
}
