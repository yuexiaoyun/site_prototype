using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace site.Common.Controls
{
    public partial class Pager1 : System.Web.UI.UserControl
    {
        #region event

        /// <summary>
        /// gets or sets the PageChanged event of the pager
        /// </summary>

        public delegate void PagerEventHandler(int currentPageIndex, int oldPageIndex);
        public event PagerEventHandler OnPageChanged;

        #endregion

        #region appearance
        //Added Ellen Guo 20070807
        public void DefaultInitPager()
        {
            ShowPageNumbers = 10;

            PreviousPageLinkText = "&lt;";
            PreviousNPagesLinkText = "...";
            FirstLinkText = "&nbsp;";

            NextPageLinkText = "&gt;";
            NextNPagesLinkText = "...";
            LastLinkText = "&nbsp;";
        }

        /// <summary>
        /// Calculate the record Number of current page and Show the message.
        /// </summary>
        public void UpdatePagerInfo(int totalCounts, int pageSize)
        {
            if (CurrentPageIndex == 0) FirstPageIndex = 0;

            int maxShownRecord = (CurrentPageIndex + 1) * pageSize;
            int startRecord = CurrentPageIndex * pageSize + 1;
            int endRecord = maxShownRecord > totalCounts ? totalCounts : maxShownRecord;

            Results = string.Format("Total {2} Results, Showing {0}-{1}", startRecord, endRecord, totalCounts);

            if (totalCounts % pageSize == 0)
            {
                TotalPageCounts = totalCounts / pageSize;
            }
            else
            {
                TotalPageCounts = totalCounts / pageSize + 1;
            }
        }

        //End of Added 20070807

        public int FirstPageIndex
        {
            get
            {
                int index = 0;
                if (ViewState[UniqueID + "FirstPageIndex"] != null)
                {
                    index = (int)ViewState[UniqueID + "FirstPageIndex"];
                }
                return index;
            }
            set
            {
                ViewState[UniqueID + "FirstPageIndex"] = value;
            }
        }

        /// <summary>
        /// gets or sets the first linkbutton text
        /// </summary>
        public string FirstLinkText
        {
            get
            {
                string firstLinkText = "First";
                if (ViewState[UniqueID + "_FirstLinkText"] != null)
                {
                    firstLinkText = ViewState[UniqueID + "_FirstLinkText"].ToString();
                }
                return firstLinkText;
            }
            set
            {
                ViewState[UniqueID + "_FirstLinkText"] = value;
            }
        }

        /// <summary>
        /// gets or sets the last linkbutton text
        /// </summary>
        public string LastLinkText
        {
            get
            {
                string lastLinkText = "Last";
                if (ViewState[UniqueID + "_LastLinkText"] != null)
                {
                    lastLinkText = ViewState[UniqueID + "_LastLinkText"].ToString();
                }
                return lastLinkText;
            }
            set
            {
                ViewState[UniqueID + "_LastLinkText"] = value;
            }
        }

        /// <summary>
        /// gets or sets the previous linkbutton text
        /// </summary>
        public string PreviousPageLinkText
        {
            get
            {
                string previousLinkText = "Previous";
                if (ViewState[UniqueID + "_PreviousLinkText"] != null)
                {
                    previousLinkText = ViewState[UniqueID + "_PreviousLinkText"].ToString();
                }
                return previousLinkText;
            }
            set
            {
                ViewState[UniqueID + "_PreviousLinkText"] = value;
            }
        }

        /// <summary>
        /// gets or sets the previous number pages linkbutton text
        /// </summary>
        public string PreviousNPagesLinkText
        {
            get
            {
                string previousNLinkText = "...";
                if (ViewState[UniqueID + "_PreviousNLinkText"] != null)
                {
                    previousNLinkText = ViewState[UniqueID + "_PreviousNLinkText"].ToString();
                }
                return previousNLinkText;
            }
            set
            {
                ViewState[UniqueID + "_PreviousNLinkText"] = value;
            }
        }

        /// <summary>
        /// gets or sets the next linkbutton text
        /// </summary>
        public string NextPageLinkText
        {
            get
            {
                string nextLinkText = "Next";
                if (ViewState[UniqueID + "_NextLinkText"] != null)
                {
                    nextLinkText = ViewState[UniqueID + "_NextLinkText"].ToString();
                }
                return nextLinkText;
            }
            set
            {
                ViewState[UniqueID + "_NextLinkText"] = value;
            }
        }

        /// <summary>
        /// gets or sets the next number pages linkbutton text
        /// </summary>
        public string NextNPagesLinkText
        {
            get
            {
                string nextNLinkText = "...";
                if (ViewState[UniqueID + "_NextNLinkText"] != null)
                {
                    nextNLinkText = ViewState[UniqueID + "_NextNLinkText"].ToString();
                }
                return nextNLinkText;
            }
            set
            {
                ViewState[UniqueID + "_NextNLinkText"] = value;
            }
        }

        /// <summary>
        /// gets or sets the count of the page number will show.
        /// </summary>
        public int ShowPageNumbers
        {
            get
            {
                int showPageNumbers = 10;
                if (ViewState[UniqueID + "_ShowPageNumbers"] != null)
                {
                    showPageNumbers = (int)ViewState[UniqueID + "_ShowPageNumbers"];
                }
                return showPageNumbers;
            }
            set
            {
                ViewState[UniqueID + "_ShowPageNumbers"] = value;
            }
        }

        /// <summary>
        /// gets or sets the total page counts
        /// </summary>
        public int TotalPageCounts
        {
            get
            {
                int totalPageCounts = 1;
                if (ViewState[UniqueID + "_TotalPageCounts"] != null)
                {
                    totalPageCounts = (int)ViewState[UniqueID + "_TotalPageCounts"];
                }
                return totalPageCounts;
            }
            set
            {
                ViewState[UniqueID + "_TotalPageCounts"] = value;
            }
        }

        /// <summary>
        /// gets or sets the index of the selected page
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                int currentPageIndex = 0;
                if (ViewState[UniqueID + "_CurrentPageIndex"] != null)
                {
                    currentPageIndex = (int)ViewState[UniqueID + "_CurrentPageIndex"];
                }
                return currentPageIndex;
            }
            set
            {
                ViewState[UniqueID + "_CurrentPageIndex"] = value;
            }
        }

        /// <summary>
        /// gets or sets the color of the selected page
        /// </summary>
        public string SelectPageIndexColor
        {
            get
            {
                string selPageIndexColor = "red";
                if (ViewState[UniqueID + "_SelectPageIndexColor"] != null)
                {
                    selPageIndexColor = ViewState[UniqueID + "_SelectPageIndexColor"].ToString();
                }
                return selPageIndexColor;
            }
            set
            {
                ViewState[UniqueID + "_SelectPageIndexColor"] = value;
            }
        }

        /// <summary>
        /// gets or sets the color of the selected page
        /// </summary>
        public FontStyle SelectPageIndexFontStyle
        {
            get
            {

                FontStyle selPageIndexFontStyle = FontStyle.Bold;
                if (ViewState[UniqueID + "_SelectPageIndexFontStyle"] != null)
                {
                    selPageIndexFontStyle = (FontStyle)ViewState[UniqueID + "_SelectPageIndexFontStyle"];
                }
                return selPageIndexFontStyle;
            }
            set
            {
                ViewState[UniqueID + "_SelectPageIndexFontStyle"] = value;
            }
        }

        public string Results
        {
            get
            {
                return this.lblResults.Text;
            }
            set
            {
                this.lblResults.Text = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            int firstPageIndex = 0;
            if (ViewState[UniqueID + "FirstPageIndex"] != null)
            {
                firstPageIndex = (int)ViewState[UniqueID + "FirstPageIndex"];
            }
            else
            {
                ViewState[UniqueID + "FirstPageIndex"] = 0;
            }

            SaveCurrentPageIndex(CurrentPageIndex);
            InitPageNumber(firstPageIndex);
            InitLinkButton();
        }

        #region method
        private void InitLinkButton()
        {
            this.lbtnFirst.Text = FirstLinkText;
            this.lbtnLast.Text = LastLinkText;
            this.lbtnNext.Text = NextPageLinkText;
            this.lbtnNext10.Text = NextNPagesLinkText;
            this.lbtnPrevious.Text = PreviousPageLinkText;
            this.lbtnPrevious10.Text = PreviousNPagesLinkText;

            this.lbtnFirst.Visible = string.IsNullOrEmpty(FirstLinkText);
            this.lbtnNext10.Visible = string.IsNullOrEmpty(NextNPagesLinkText);
            this.lbtnPrevious10.Visible = string.IsNullOrEmpty(PreviousNPagesLinkText);
            this.lbtnLast.Visible = string.IsNullOrEmpty(LastLinkText);
        }

        private void InitPageNumber(int startPageIndex)
        {
            int realStartPageIndex = startPageIndex > TotalPageCounts ? 0 : startPageIndex;
            pagerTable.Visible = (TotalPageCounts != 0);
            int start = realStartPageIndex;
            int end = realStartPageIndex + ShowPageNumbers > TotalPageCounts
                          ? TotalPageCounts
                          : realStartPageIndex + ShowPageNumbers;

            SaveCurrentFirstPageIndex(start);

            panelPageNumber.Controls.Clear();

            HtmlGenericControl spanFirst = new HtmlGenericControl("span");
            spanFirst.ID = string.Format("span{0}", -1);
            spanFirst.InnerHtml = "&nbsp;";

            panelPageNumber.Controls.Add(spanFirst);

            for (int i = start; i < end; i = i + 1)
            {
                string pageNumber = Convert.ToString(i + 1);
                if (i == CurrentPageIndex)
                {
                    string currentPageStyle = string.Empty;
                    currentPageStyle = string.Format("color:{0};font-weight:{1}", SelectPageIndexColor, SelectPageIndexFontStyle.ToString());
                    HtmlGenericControl currentPageText = new HtmlGenericControl("span");
                    currentPageText.ID = string.Format("lbtn{0}", i - startPageIndex);
                    currentPageText.InnerHtml = pageNumber;
                    currentPageText.Attributes.Add("style", currentPageStyle);
                    panelPageNumber.Controls.Add(currentPageText);
                }
                else
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = string.Format("lbtn{0}", i - realStartPageIndex);
                    lbtn.Command += new CommandEventHandler(Pager_Command);
                    lbtn.CommandName = i.ToString();
                    lbtn.Text = string.Format("{0}", pageNumber);

                    panelPageNumber.Controls.Add(lbtn);
                }

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.ID = string.Format("span{0}", i - realStartPageIndex);
                span.InnerHtml = "&nbsp;";

                panelPageNumber.Controls.Add(span);
            }

            InitPageLinkButtonState(CurrentPageIndex);
        }

        void InitPageLinkButtonState(int pageIndex)
        {
            this.lbtnFirst.Enabled = (pageIndex != 0);
            this.lbtnPrevious.Enabled = (pageIndex != 0);
            this.lbtnPrevious10.Enabled = (pageIndex != 0);

            this.lbtnNext10.Enabled = (pageIndex != (TotalPageCounts - 1));
            this.lbtnNext.Enabled = (pageIndex != (TotalPageCounts - 1));
            this.lbtnLast.Enabled = (pageIndex != (TotalPageCounts - 1));
        }

        protected void Pager_Command(object sender, CommandEventArgs e)
        {
            string commandname = e.CommandName.ToLower();
            int firstPageIndex = 0;
            int pageIndex = 0;
            int currentPageNumber = 0;
            int currentFirstPageNumber = 0;

            if (ViewState[UniqueID + "FirstPageIndex"] != null)
            {
                firstPageIndex = (int)ViewState[UniqueID + "FirstPageIndex"];
            }

            int.TryParse(this.lblCurrentPageNumber.Text, out currentPageNumber);
            int.TryParse(this.lblCurrentFirstPageNumber.Text, out currentFirstPageNumber);

            switch (commandname)
            {
                case "first":
                    pageIndex = 0;
                    break;
                case "previous":
                    pageIndex = currentPageNumber;
                    if (currentPageNumber > 0)
                    {
                        pageIndex = currentPageNumber - 1;
                    }
                    break;
                case "previous10":
                    firstPageIndex = currentFirstPageNumber - ShowPageNumbers > 0 ? currentFirstPageNumber - ShowPageNumbers : 0;
                    pageIndex = (currentFirstPageNumber - 1) > 0 ? currentFirstPageNumber - 1 : 0;

                    break;
                case "next10":
                    firstPageIndex = currentFirstPageNumber + ShowPageNumbers > TotalPageCounts ? TotalPageCounts - ShowPageNumbers : currentFirstPageNumber + 10;
                    pageIndex = firstPageIndex;
                    break;
                case "next":
                    pageIndex = currentPageNumber;
                    if (currentPageNumber < TotalPageCounts - 1)
                    {
                        pageIndex = currentPageNumber + 1;
                    }
                    break;
                case "last":
                    pageIndex = TotalPageCounts - 1;
                    firstPageIndex = TotalPageCounts - ShowPageNumbers;
                    if (firstPageIndex < 0) firstPageIndex = 0;
                    break;
                default:
                    int.TryParse(commandname, out pageIndex);
                    break;
            }

            if (pageIndex < firstPageIndex || pageIndex >= firstPageIndex + ShowPageNumbers)
            {
                firstPageIndex = pageIndex;
            }

            ViewState[UniqueID + "FirstPageIndex"] = firstPageIndex;

            SaveCurrentPageIndex(pageIndex);

            //invoke the pagechanged
            PageChanged(pageIndex, currentPageNumber);
        }

        private void SaveCurrentFirstPageIndex(int pageIndex)
        {
            this.lbtnPrevious10.Visible = (pageIndex > 0);
            this.lbtnNext10.Visible = (pageIndex + ShowPageNumbers < TotalPageCounts);

            this.lblCurrentFirstPageNumber.Text = pageIndex.ToString();
        }

        private void SaveCurrentPageIndex(int pageIndex)
        {
            this.lblCurrentPageNumber.Text = pageIndex.ToString();
            CurrentPageIndex = pageIndex;

            int firstPageIndex = 0;
            if (ViewState[UniqueID + "FirstPageIndex"] != null)
            {
                firstPageIndex = (int)ViewState[UniqueID + "FirstPageIndex"];
            }

            InitPageNumber(firstPageIndex);
        }

        /// <summary>
        /// invoke the PageChanged, in the event,client can get the current page index and handle the data bind.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="oldPageIndex"></param>
        private void PageChanged(int pageIndex, int oldPageIndex)
        {
            if (OnPageChanged != null)
            {
                OnPageChanged(pageIndex, oldPageIndex);
            }
        }

        public void Refresh()
        {
            int firstPageIndex = 0;
            if (ViewState[UniqueID + "FirstPageIndex"] != null)
            {
                firstPageIndex = (int)ViewState[UniqueID + "FirstPageIndex"];
            }
            InitPageNumber(firstPageIndex);
        }

        #endregion
    }
}