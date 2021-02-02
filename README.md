# PhilosopyBlogMVC
.Net Framework MVC BLog 

<strong>Summary</strong>

It is a blog website project which is developed by me on .Net Framework MVC. In the project i used one-to-many and many-to-many related tables and created a database with the code first approach using Entity Framework. Also i used MSSQL.There are authentication operations in the project and in this way site user can register and create a articles, comment,report also like the articles. Here is the published link <a>https://blog.tayran.online/</a>

<strong>Structure And Code Documentation</strong>


<img src="/BlogMVC/blogreadme_md/blog1.PNG/">


<strong>View Codes</strong>
```html
  <div class="pageheader-content row">
        <div class="col-full">

            <div class="featured">
                @foreach (var item in Model.OrderByDescending(x => x.Likes.Count()).Take(1).ToList())
                {
                    <div class="featured__column featured__column--big">
                        @if (!string.IsNullOrEmpty(item.PhotoPath))
                        {
                            <div class="entry" style="background-image: url('@Url.Content(item.PhotoPath).Replace(@"\","/")')">
                                <div class="entry__content">
                                    <span class="entry__category">
                                        @foreach (var category in item.Categories.Take(1))
                                        {
                                            @Html.ActionLink(category.Name, "Index", "Category", new { categoryName = category.Name }, null)
                                        }
                                    </span>
                                    <h1><a href="@Url.Action("Index", "Article", new { id = item.Id })" title="">@item.Title</a></h1>
                                    <div class="entry__info">
                                        <a href="#0" class="entry__profile-pic">
                                            <img class="avatar" src="~/images/avatars/user-03.jpg" alt="">
                                        </a>
                                        <ul class="entry__meta">
                                            <li><a href="#0">@item.ApplicationUser.NickName</a></li>
                                            <li>@item.ArticleTime.Value.ToString("dddd, dd MMMM yyyy")</li>
                                        </ul>
                                    </div>
                                </div> <!-- end entry__content -->

                            </div> <!-- end entry -->
                        }
                    </div> <!-- end featured__big -->
                }
                <div class="featured__column featured__column--small">
                    @foreach (var item in Model.OrderByDescending(x => x.Likes.Count()).Skip(1).Take(2).ToList())
                    {
                        <div class="entry" style="background-image:url('@Url.Content(item.PhotoPath).Replace(@"\","/")')">
                            <div class="entry__content">
                                <span class="entry__category">
                                    @foreach (var category in item.Categories.Take(1))
                                    {
                                        @Html.ActionLink(category.Name, "Index", "Category", new { categoryName = category.Name }, null)
                                    }
                                </span>
                                <h1><a href="@Url.Action("Index", "Article", new { id = item.Id })" title="">@item.Title</a></h1>

                                <div class="entry__info">
                                    <a href="#0" class="entry__profile-pic">
                                        <img class="avatar" src="'~/images/avatars/user-03.jpg" alt="">
                                    </a>
                                    <ul class="entry__meta">
                                        <li><a href="#0">@item.ApplicationUser.NickName</a></li>
                                        <li>@item.ArticleTime.Value.ToString("dddd, dd MMMM yyyy")</li>
                                    </ul>
                                </div>
                            </div> <!-- end entry__content -->
                        </div> <!-- end entry -->
                    }
                </div> <!-- end featured__small -->
            </div> <!-- end featured -->
        </div> <!-- end col-full -->
      </div>
```    
   <pre> <img src="/BlogMVC/blogreadme_md/2.PNG/"> </pre>
<strong>View Codes</strong>

```html

<section class="s-content">
    <div class="row masonry-wrap">
        <div class="masonry" >

            @foreach (var article in Model)
            {
                <article class="masonry__brick entry format-standard" data-aos="fade-up" data-string="@article.Title">

                    @if (!string.IsNullOrEmpty(article.PhotoPath))
                    {
                        <div class="entry__thumb">
                            <a href="@Url.Action("Index", "Article", new { id = article.Id })" class="entry__thumb-link">
                                <img src="@article.PhotoPath" alt="@article.Title">
                            </a>
                        </div>
                    }

                    <div class="entry__text">
                        <div class="entry__header">

                            <div class="entry__date">
                                <a href="single-standard.html">@article.ArticleTime.Value.ToString("dddd, MMMM d, yyyy")</a>
                            </div>
                            <h1 class="entry__title"><a href="@Url.Action("Index", "Article", new { id = article.Id })">@article.Title</a></h1>
                        </div>
                        <div class="entry__excerpt" style="max-height:300px; overflow:hidden">
                            @Html.Raw(article.Summary)
                        </div>
                        <div class="entry__meta">
                            <span class="entry__meta-links">
                                @foreach (var item in article.Categories)
                                {
                                    @Html.ActionLink(item.Name, "Index", "Category", new { categoryName = item.Name }, null)
                                }
                            </span>
                        </div>
                    </div>

                </article> <!-- end article -->
            }
        </div> <!-- end masonry -->
    </div> <!-- end masonry-wrap -->
   <pre> <img src="/BlogMVC/blogreadme_md/3.PNG/"> </pre>
   <strong>View Codes</strong>

    <div class="row">
        <div class="col-full">
            <nav class="pgn">
                Page @(Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber) of @Model.PageCount
                @Html.PagedListPager(Model, page => Url.Action("Index", "Home", new { page }))
            </nav>
        </div>
    </div>
    <div class="col-eight md-six tab-full popular">
                <h3>Popular Posts</h3>

                @Html.Action("PopularPostPartial")
    </div> <!-- end popular -->
    
 ```
 
 <pre> <img src="/BlogMVC/blogreadme_md/4.PNG/"> </pre>
 <pre> <img src="/BlogMVC/blogreadme_md/5.PNG/"> </pre>
 <pre> <img src="/BlogMVC/blogreadme_md/6.PNG/"> </pre>

<strong>Controller Codes</strong>

<code>

    {
        // GET: Article
        //[Route("Article/{id}")]
        public ActionResult Index(int? id)
        {
            ViewBag.reportTypes = new SelectList(db.ReportCategories.ToList(), "Id", "Name");
            ViewBag.Active = "MyArticle";
            Article article = db.Articles.FirstOrDefault(x => x.Id == id);
            ViewBag.popularArticle = db.Articles.ToList();
            return View(article);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(AddCommentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    ArticleId = vm.ArticleId,
                    Content = vm.Content,
                    ApplicationUserId = User.Identity.GetUserId(),
                    CommentTime = DateTime.Now
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                ApplicationUser user = db.Users.Find(comment.ApplicationUserId);
                CommentAddedViewModel result = new CommentAddedViewModel()
                {
                    CommentSuccess = "Your commend successfuly added.",
                    Author = user.NickName,
                    Content = comment.Content,
                    CommentId = comment.Id,
                    CommentTime = comment.CommentTime.ToString("g")
                };
                return Json(result);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { ErrorMessage = "Please control your last message" });
        }

        [HttpPost]
        public ActionResult SendReport(int? articleId, int? reportType)
        {
            if (reportType == null)
            {
                return HttpNotFound();
            }
            Report report = new Report();
            report.ApplicationUserId = User.Identity.GetUserId();
            report.Article = db.Articles.Find(articleId);
            report.ReportCategory = db.ReportCategories.Find(reportType);
            report.ReportTime = DateTime.Now;
            report.IsInvestigated = false;
            db.Reports.Add(report);
            db.SaveChanges();
            return Json(new EmptyResult());
        }

        [HttpPost]
        public ActionResult Like(int? LikedArticleId)
        {
            if (LikedArticleId == null)
            {
                HttpNotFound();
            }
            string userId = User.Identity.GetUserId();

            if (db.Users.FirstOrDefault(x => x.Id == userId).Likes.Any(x => x.ArticleId == LikedArticleId))
            {
                ApplicationUser user = db.Users.Find(userId);
                Like like = db.Likes.ToList().Where(x => x.ApplicationUser == user).FirstOrDefault(x => x.ArticleId == LikedArticleId);
                Article article = db.Articles.Find(LikedArticleId);
                user.Likes.Remove(like);
                article.Likes.Remove(like);
                db.Likes.Remove(like);
                db.SaveChanges();
                return Json("Unlike");
            }
            else
            {
                Like like = new Like();
                ApplicationUser user = db.Users.Find(userId);
                like.Islike = true;
                user.Likes.Add(like);
                Article article = db.Articles.Find(LikedArticleId);
                article.Likes.Add(like);
                db.Likes.Add(like);
                db.SaveChanges();
                return Json("Like");
            }
        }

    }
</code>

<strong>View Codes</strong>

```html

<section class="s-content s-content--narrow s-content--no-padding-bottom">

    <article class="row format-standard">

        <div class="s-content__header col-full">
            <h1 class="s-content__header-title">
                @Model.Title
            </h1>
            <ul class="s-content__header-meta">
                <li class="date">@Model.ArticleTime.Value.ToString("dddd, MMMM d, yyyy")</li>
                <li class="cat">
                    In
                    @foreach (var item in Model.Categories)
                    {
                        <a href="#0">@item.Name</a>
                    }
                </li>
            </ul>
        </div> <!-- end s-content__header -->
        @if (!String.IsNullOrEmpty(Model.PhotoPath))
        {
            <div class="s-content__media col-full" style="justify-content:center; display:flex;">
                <div class="s-content__post-thumb">
                    <img src="@Model.PhotoPath" alt="@Model.Title" />
                </div>
            </div> <!-- end s-content__media -->
        }

        <div class="col-full s-content__main ">
            <p class="drop-cap">
                @Html.Raw(Model.Content)
            </p>

            @if (User.Identity.IsAuthenticated)
            {

                <div class="full-width position-absolute d-flex">
                    @if (Model.Likes.Any(x => x.ApplicationUser.UserName == HttpContext.Current.User.Identity.Name))
                    {
                        <a href="#" id="likeButton" data-article-like-id="@Model.Id" style="color:black"><i class="fas fa-thumbs-up" style="font-size:x-large; margin-right:10px;"></i></a>
                    }
                    else
                    {
                        <a href="#" id="likeButton" data-article-like-id="@Model.Id"><i class="fas fa-thumbs-up" style="font-size:x-large; margin-right:10px;"></i></a>
                    }
                    <span class="ml-1" id="LikeCount">@Model.Likes.Count()</span>
                </div>
                <a href="#ex1" rel="modal:open"><i class="fas fa-flag" style="font-size:x-large"></i></a>

                <div id="ex1" class="modal">
                    <form id="frmSendReport" method="post">
                        <p><strong>Report Reason</strong></p>
                        @Html.Hidden("articleId", Model.Id)
                        @Html.DropDownList("reportType", (SelectList)ViewBag.reportTypes, new { @class = "full-width" })
                        <div class="text-center">
                            <a href="#" rel="modal:close" onclick="$('#frmSendReport').submit()" class="btn btn--pill">Send</a>
                        </div>
                    </form>
                </div>

            }
            <p class="s-content__tags ">
                <span>Post Categories</span>

                <span class="s-content__tag-list">
                    @foreach (var item in Model.Categories)
                    {
                        <a href="#0">@item.Name</a>
                    }

                </span>
            </p> <!-- end s-content__tags -->

            <div class="s-content__author ">
                <br />
                <img src="~/images/avatars/user-03.jpg" alt="">
                <div class="s-content__author-about">
                    <h4 class="s-content__author-name">
                        <a href="#0">@Model.ApplicationUser.NickName</a>
                    </h4>
                </div>
            </div>
        </div> <!-- end s-content__main -->

    </article>

    <!-- comments
    ================================================== -->
    <div class="comments-wrap">

        <div id="comments" class="row">
            <div class="col-full">

                <h2 class="h2">
                    <span id="commentCount">@Model.Comments.Count </span> Comments
                </h2>

                <!-- commentlist -->
                <ol class="commentlist">
                    @if (!Model.Comments.Any())
                    {
                        <p class="text-center noComment">There is no comment yet.Add first comment.</p>
                    }
                    else
                    {
                        foreach (Comment comment in Model.Comments.OrderByDescending(x => x.CommentTime))
                        {
                            <li class="depth-1 comment" id="commentli">
                                <div class="comment__avatar">
                                    <img width="50" height="50" class="avatar" src="https://picsum.photos/50/50" alt="">
                                </div>
                                <div class="comment__content" id="comment__content">

                                    <div class="comment__info">
                                        <cite id="commentUserName">@comment.ApplicationUser.NickName</cite>

                                        <div class="comment__meta">
                                            <time class="comment__time" id="commentTime">@comment.CommentTime.ToString("dddd, MMMM d, yyyy")</time>
                                        </div>
                                    </div>

                                    <div class="comment__text">
                                        <p id="contentArea">
                                            @comment.Content
                                        </p>
                                    </div>

                                </div>
                            </li>
                            <hr />
                        }
                    }
                </ol> <!-- end commentlist -->
                <!-- respond
                ================================================== -->
                <div class="respond">

                    <h3 class="h2">Add Comment</h3>

                    @if (User.Identity.IsAuthenticated)
                    {
                        @Html.Partial("_CommentPartial", new AddCommentViewModel { ArticleId = Model.Id })
                    }
                    else
                    {
                        <p class="text-center my-0">
                            For Add New Comment You Have To <a href="@Url.Action("Login", "Account",
                                          new { returnUrl = this.Request.RawUrl })"><span style="font:bold">Login</span></a> First
                        </p>
                    }

                </div> <!-- end respond -->

            </div> <!-- end col-full -->

        </div> <!-- end row comments -->
    </div> <!-- end comments-wrap -->

</section> <!-- s-content -->
```

<strong>Script Codes</strong>

   ```html
    <script>
     $("#contactForm").submit(function (e) {
            e.preventDefault();

                $.ajax({
                    type: "post",
                    url: "@Url.Action("AddComment","Article")",
                    data: $("#contactForm").serialize(),
                    success: function (receivedData) {
                        toastr.success(receivedData.CommentSuccess);
                        $(".noComment").hide();
                        $("#cMessage").val("");

                        var count = parseInt($("#commentCount").text());
                        $("#commentCount").text(count + 1);
                        var p = $("<p/>").text(receivedData.Content);
                        var cite = $("<cite/>").text(receivedData.Author);
                        var time = $("<time/>").text(receivedData.CommentTime);
                        var div0 = $("<div/>").addClass("comment__content")
                        var div1 = $("<div/>").addClass("comment__info").append(cite);
                        var div2 = $("<div/>").addClass("comment__meta").append(time);
                        var div3 = $("<div/>").addClass("comment__text").append(p);
                        var div4 = div1.append(div2);
                        var div5 = div0.append(div4).append(div3);
                        var img = $("<img/>").addClass("avatar").attr({ height: "50", width: "50", src: "https://picsum.photos/50/50" });
                        var divimg = $("<div/>").addClass("comment__avatar").append(img);
                        var li = $("<li/>").addClass("depth-1 comment").attr("id", "commentli").prepend("<hr/>").prepend(div5).prepend(divimg);
                        $(".commentlist").prepend(li);
                    }
                });
        });
        $("#frmSendReport").submit(function (e) {
            e.preventDefault();

            $.ajax({
                type: "post",
                url: "@Url.Action("SendReport","Article")",
                data: $("#frmSendReport").serialize(),
                success: function (data) {
                    toastr.success("Report successfuly send")
                },
                error: function () {
                    toastr.er("Report is not send to admins.")
                }
            });
        });
        $("body").on("click","[data-article-like-id]", function (e) {
            e.preventDefault();
            var likedArticleId = $(this).data("article-like-id")

            var likeCount = parseInt($("#LikeCount").text())
            $.ajax({
                type: "post",
                url: "@Url.Action("Like","Article")",
                data: { LikedArticleId : likedArticleId },
                success: function (data) {
                    switch (data) {
                        case "Unlike":
                            $("#likeButton").css("color", "#0058A5");
                            $("#LikeCount").text(likeCount - 1)

                            break;
                        case "Like":
                            $("#likeButton").css("color", "black");
                            $("#LikeCount").text(likeCount + 1)
                            break;
                    }

                },
                error: function () {
                    toastr.error("Error occured while liking")
                }
            });
        });
    </script>
```
    
<pre> <img src="/BlogMVC/blogreadme_md/7.PNG/"> </pre>   
<pre> <img src="/BlogMVC/blogreadme_md/8.PNG/"> </pre>
<pre> <img src="/BlogMVC/blogreadme_md/15.PNG/"> </pre>

  
<strong>Controller Codes</strong>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewArticle(Article article, HttpPostedFileBase file, int[] cats)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in cats)
                {
                    var category = db.Categories.Find(item);
                    article.Categories.Add(category);
                }
                var supportedTypes = new string[] { "jpg", "jpeg", "png" };

                if (file != null)
                {
                    var type = System.IO.Path.GetExtension(file.FileName).ToLower().Replace(".", "");
                    if (supportedTypes.Contains(type) && file != null)
                    {
                        string delimiter = Guid.NewGuid().ToString();
                        string path = @"\UploadedFile\" + delimiter + "_" + file.FileName;
                        file.SaveAs(Server.MapPath("~") + path);
                        article.PhotoPath = path;
                    }
                }
                article.ApplicationUserId = User.Identity.GetUserId();
                article.ArticleTime = DateTime.Now;
                db.Entry(article).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("NewArticle");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            TempData["DeleteArticle"] = "Success";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ViewBag.cats = new MultiSelectList(db.Categories.ToList(), "Id", "Name");
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(Article article, HttpPostedFileBase file, int[] cats, string Title, string Content, string Summary)
        {
            article = db.Articles.FirstOrDefault(x => x.Id == article.Id);
            if (ModelState.IsValid)
            {
                foreach (var item in cats)
                {
                    var category = db.Categories.Find(item);
                    article.Categories.Add(category);
                }
                var supportedTypes = new string[] { "jpg", "jpeg", "png" };
                if (file != null)
                {
                    var type = System.IO.Path.GetExtension(file.FileName).ToLower().Replace(".", "");
                    if (supportedTypes.Contains(type))
                    {
                        string delimiter = Guid.NewGuid().ToString();
                        string path = @"\UploadedFile\" + delimiter + "_" + file.FileName;
                        file.SaveAs(Server.MapPath("~") + path);
                        article.PhotoPath = path;
                    }
                }
                else
                {
                    article.PhotoPath = db.Articles.FirstOrDefault(x => x.Id == article.Id).PhotoPath;
                }
                article.Summary = Summary;
                article.Title = Title;
                article.Content = Content;
                article.ApplicationUserId = User.Identity.GetUserId();
                article.ArticleTime = DateTime.Now;
                //db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["EditArticle"] = "Error";
            return View();
        }
    }
    
<strong>View Codes</strong>

```html

<section class="s-content">
    <div class="row text-center">
        @Html.ActionLink("New Article", "NewArticle", "Profile", new { @class = "btn btn--primary" })
    </div>
    @if (Model.Count == 0)
    {
        <div class="row text-center">
            <hr />
            <p>
                You don't have an article yet. Would you like to create a new one right now?
            </p>
        </div>
    }
    @foreach (var item in Model)
    {
        <div class="row " style=" word-break: break-all; ">
            <div class="col-md-12 full-width">
                <h4>@item.Title</h4>
                <div class="entry__excerpt">
                    @item.Summary
                </div>
                <p>@item.ArticleTime.Value.ToString("dddd, MMMM d, yyyy")</p>
                <a href="@Url.Action("Index","Article",new {id=item.Id})"><i class="fas fa-directions" id="goToArticle">Go To Article</i> </a>
                <div class="text-right">
                    <a href="@Url.Action("Delete","Profile",new {id = item.Id})" id="btnDelete"><i class="fas fa-trash-alt tm-trash-icon" style="font-size:x-large;"></i></a>
                    <a href="@Url.Action("Edit","Profile",new {id = item.Id })"><i class="fas fa-edit tm-edit-icon" style="font-size:x-large"></i></a>
                </div>
            </div>
        </div>
        <hr />
    }

</section>
```
<strong>Script Codes</strong>
    
 ```html
     <script>
        $("#btnDelete").click(function () {

            if (!confirm("Your article will be delete after this process ?")) {
                return false;
            };
        });

        var deleteArticleProcess = "@TempData["DeleteArticle"]";
        if (deleteArticleProcess) {
            toastr.success("Your Article has been successfuly removed")
        }


    </script>
 ```
<pre> <img src="/BlogMVC/blogreadme_md/9.PNG/"> </pre>
<pre> <img src="/BlogMVC/blogreadme_md/10.PNG/"> </pre>
<strong>Controller Codes</strong>

<code>

          public ActionResult Index()
          {
            List<DataPoint> dataPoints = new List<DataPoint>();
            dataPoints.Add(new DataPoint("Software", db.Categories.FirstOrDefault(x => x.Name == "Software").Articles.Count()));
			      dataPoints.Add(new DataPoint("Sports", db.Categories.FirstOrDefault(x => x.Name == "Sports").Articles.Count()));
			      dataPoints.Add(new DataPoint("Science", db.Categories.FirstOrDefault(x => x.Name == "Science").Articles.Count()));
			      dataPoints.Add(new DataPoint("Politics", db.Categories.FirstOrDefault(x => x.Name == "Politics").Articles.Count()));
			      dataPoints.Add(new DataPoint("Movie", db.Categories.FirstOrDefault(x => x.Name == "Movie").Articles.Count()));
			      dataPoints.Add(new DataPoint("Research", db.Categories.FirstOrDefault(x => x.Name == "Research").Articles.Count()));
			      dataPoints.Add(new DataPoint("Lifestyle", db.Categories.FirstOrDefault(x => x.Name == "Lifestyle").Articles.Count()));
			      ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.active = "Dashboard";
            return View();
          }
        
</code>

 
<pre> <img src="/BlogMVC/blogreadme_md/11.PNG/"></pre>


<strong>Controller Codes</strong>

<code>

        // GET: Admin/Article
        public ActionResult Index(string status)
        {
            ViewBag.categorySuccess = status;
            ViewBag.active = "Articles";
            ViewBag.adminCategories = db.Categories.ToList();
            return View(db.Articles.ToList());
        }
        
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteArticle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            db.Articles.Remove(article);
            db.SaveChanges();
            TempData["ArticleDelete"] = "success";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddCategory(string categoryName, string description)
        {
            string status = "";
            if (categoryName != null && description != null)
            {
                Category category = new Category();
                category.Name = categoryName.Trim();
                category.Description = description.Trim();
                db.Entry(category).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                status = "Category Successfuly Added";
            }
            else
            {
                status = "Category is not Added";
            }
            return RedirectToAction("Index", new { status = status });
        }

        public JsonResult EditCategory(string id)
        {
            int categoryId = Convert.ToInt32(id.Substring(4));
            Category category = db.Categories.Find(categoryId);
            return Json(new { Id = category.Id, Description = category.Description, Name = category.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditCategory(int catId, string editCategoryName, string editDescription)
        {
            TempData["status"] = "error";
            if (catId != 0 && editCategoryName != null && editDescription != null && editCategoryName.Length <= 30 && editCategoryName.Length <= 300)
            {
                Category category = db.Categories.Find(catId);
                category.Name = editCategoryName.Trim();
                category.Description = editDescription.Trim();
                db.SaveChanges();
                TempData["status"] = "success";
            }
            return RedirectToAction("Index");
        }

        public ActionResult ArticleDetails(int? id,bool? IsInvestigated,int? reportId)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            if (IsInvestigated != null && reportId != null)
            {
                Report report = db.Reports.Find(reportId);
                report.IsInvestigated = IsInvestigated;
                db.SaveChanges();
            }
            return View(article);
        }

        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Comment comment = db.Comments.Find(id);
            
            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Entry(comment).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            TempData["commentProcess"] = "true";
            return RedirectToAction("ArticleDetails",new {id = comment.ArticleId });
        }
</code>


<strong>View Codes</strong>

```html

<div class="row tm-content-row tm-mt-big">
    <div class="col-xl-8 col-lg-12 tm-md-12 tm-sm-12 tm-col">
        <div class="bg-white tm-block h-100">
            <div class="row">
                <div class="col-md-8 col-sm-12">
                    <h2 class="tm-block-title d-inline-block">Articles</h2>
                </div>
            </div>
            <div class="table-responsive">
                <table class="table table-hover table-striped tm-table-striped-even mt-3" id="myTable">
                    <thead>
                        <tr class="tm-bg-gray">
                            <th scope="col">Name</th>
                            <th scope="col">Author</th>
                            <th scope="col" class="text-center">#Comment</th>
                            <th scope="col">Time</th>
                            <th scope="col" class="text-center">Categories</th>
                            <th scope="col">&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach (var article in Model)
                        {
                            <tr >
                                <td class="tm-product-name"><a href="@Url.Action("ArticleDetails","AdminArticle", new {id = article.Id })"><i class="fas fa-arrow-right"></i>                                   <span class="tm-link-black">@article.Title</span></a></td>
                                <td class="text-left">@article.ApplicationUser.NickName</td>
                                <td class="text-center">@article.Comments.Count()</td>
                                <td>@article.ArticleTime.Value.ToString("dddd, MMMM d, yyyy")</td>
                                <td class="text-center">@string.Join(" ", article.Categories.Select(x => x.Name))</td>
                                <td>
                                    <a href="@Url.Action("DeleteArticle","AdminArticle",new { id=article.Id})" id="btnDeleteArticle" class="btnDeleteArticle">
                                        <i class="fas fa-trash-alt tm-trash-icon"></i>
                                    </a>
                                </td>
                            </tr>
                        }
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <div class="col-xl-4 col-lg-12 tm-md-12 tm-sm-12 tm-col">
        <div class="bg-white tm-block h-100">
            <h2 class="tm-block-title d-inline-block">Article Categories</h2>
            <table class="table table-hover table-striped mt-3">
                <tbody>
                    @foreach (var category in ViewBag.adminCategories)
                    {
                        <tr>
                            <td>@category.Name</td>
                            <td class="tm-trash-icon-cell "><a href="@Url.Action("DeleteCategory","AdminArticle",new {id= category.Id})" class="btnCategoryDelete"><i class="fas                              fa-trash-alt tm-trash-icon"></i></a></td>
                            <td class="tm-trash-icon-cell pl-0">
                                <a href="#" id="cat-@category.Id" class="btnEditCategory" data-toggle="modal" data-target="#myEditModal"><i class="fas fa-edit tm-edit-icon">                                 </i></a>
                            </td>
                        </tr>
                    }
                </tbody>
            </table>
            <a href="#" class="btn btn-primary tm-table-mt" data-toggle="modal" data-target="#myModal">Add New Category </a>
        </div>
    </div>
</div>

<form action="@Url.Action("AddCategory","AdminArticle")" method="post" id="frmAddCategory">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">Add New Category</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    @Html.AntiForgeryToken()
                    @Html.Label("Category Name")
                    @Html.TextBox("categoryName", null, new { @class = "form-control", required = "required", maxlength = 30 })
                    @Html.Label("Description")
                    @Html.TextArea("description", null, new { @class = "form-control", required = "required", maxlength = 300 })
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="submit" class="btn btn-primary">Add</button>
                </div>
            </div>
        </div>
    </div>
</form>

<form id="frmEditCategory" action="@Url.Action("EditCategory","AdminArticle")" method="post">
    <div class="modal fade" id="myEditModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">Edit Category</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    @Html.AntiForgeryToken()
                    @Html.Hidden("catId", "")
                    @Html.Label("Category Name")
                    @Html.TextBox("editCategoryName", null, new { @class = "form-control", required = "required", maxlength = 30, minlength = 3 })
                    @Html.Label("Category Description")
                    @Html.TextArea("editDescription", null, new { @class = "form-control", required = "required", maxlength = 300, minlength = 5 })
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    <button type="submit" class="btn btn-primary">Save</button>
                </div>
            </div>
        </div>
    </div>
</form>

```

<strong>Script Codes</strong>

```html

<script>
        $(".btnCategoryDelete").click(function () {

            if (!confirm("Selected article will be deleted after this.")) {
                return false;
            };
        });
        var articleDeleteProcess = "@TempData["ArticleDelete"]";
        if (articleDeleteProcess) {
            toastr.success("Selected Article has been removed successfully.")
        }

        $(".btnDeleteArticle").click(function () {

            if (!confirm("Selected article will be deleted after this.")) {
                return false;
            };

        });

        var view = "@ViewBag.categorySuccess"
        var viewEdit = "@ViewBag.categoryEditStatus"

        if (view) {
            switch (view) {
                case "Category Successfuly Added":
                    toastr.success("Category Successfuly Added")
                    break;
                case "Category is not Added":
                    toastr.error("Category is not Added")
                    break;
            }
        }

   
        $(document).ready(function () {
            $('#myTable').DataTable();
        });

        var status = "@TempData["status"]";
        if (status)
        {
            switch (status) {

                case "error":
                    toastr.error("An error occurred while processing your request!");
                    break;
                case "success":
                    toastr.success("Your requsted has been completed successfully.");
                    break;
            }
        }

        var selectedCatId = "";
        $(".btnEditCategory").click(function () {

            selectedCatId = $(this).attr('id');

            $.ajax({
                type: "GET",
                url: "@Url.Action("EditCategory", "AdminArticle")",
                data: { id: selectedCatId},
                success: function (data) {

                    $('input[name=catId]').val(data.Id);
                    $("#editCategoryName").val(data.Name);
                    $("#editDescription").val(data.Description);
                },
                error: function () {
                    alert("User Not Found");
                    location.reload();
                }
            });
        });

        $('#myTable').DataTable({ "order": [[3, "desc"]] });

        

    </script>
    
```

<pre> <img src="/BlogMVC/blogreadme_md/12.PNG/"></pre>
<pre> <img src="/BlogMVC/blogreadme_md/13.png/"></pre>
    
    
<strong>Controller Codes</strong>
    
<code>

        // GET: Admin/AdminReport
        public ActionResult Index()
        {
            ViewBag.active = "Reports";
            return View(db.Reports.ToList());
        }

        public ActionResult NotificationPartial() 
        {
            return PartialView("_NotificationPartial", db.Reports.ToList());
        }
        
</code>

<strong>View Codes</strong>

```html

    @model List<Report>
    @{
    ViewBag.Title = "Index";
    }

<div class="container bg-white mt-5" style="overflow:scroll">
    <h2 class="text-primary text-center">Reports</h2>
    <div>
        <table class="table" id="myTable">
            <thead>
                <tr>
                    <th>Article Name</th>
                    <th>Report Category</th>
                    <th>Reporter Name</th>
                    <th>Report Time</th>
                    <th>Go To Article Details</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var report in Model)
                {
                    <tr>
                        <td>@report.Article.Title</td>
                        <td>@report.ReportCategory.Name</td>
                        <td>@report.ApplicationUser.NickName</td>
                        <td>
                            @if (report.ReportTime != null)
                            {
                                @report.ReportTime.Value.ToString("g");
                            }

                        </td>
                        <td class="tm-product-name">
                            @if (report.IsInvestigated == true)
                            {
                                <a href="@Url.Action("ArticleDetails","AdminArticle", new {id = report.ArticleId,isInvestigated = true,reportId = report.Id})" class="ml-5"                                 style="color:green"><i class="fas fa-arrow-right"></i></a>
                            }
                            else
                            {
                                <a href="@Url.Action("ArticleDetails","AdminArticle", new {id = report.ArticleId,isInvestigated = true,reportId = report.Id})" class="ml-5" s                                     style="color:red"><i class="fas fa-arrow-right"></i></a>
                            }
                        </td>

                    </tr>
                }
            </tbody>
        </table>
    </div>
</div>

```

<strong>View Scripts</strong>

```html

@section Scripts{
    <script>
        $(document).ready(function () {
            $('#myTable').DataTable();
        });
        $('#myTable').DataTable({ "order": [[3, "desc"]] });
    </script>
}

```

 <pre> <img src="/BlogMVC/blogreadme_md/14.PNG/"> </pre>
 
 <strong>Controller Codes</strong>

<code>

    [Authorize(Roles = "admin")]
    public class AdminAccountController : BaseController
    {
        // GET: Admin/AdminAccount
        public ActionResult Index(string result)
        {
            ViewBag.Active = "Accounts";
            ViewBag.result = result;
            var data = db.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.NickName,
                Email = x.Email,
                Roles = x.Roles.Join(db.Roles, userRole => userRole.RoleId, role => role.Id, (t1, t2) => t2.Name).ToList()
            }).ToList();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GiveAdminRole(string userId, bool IsAdmin)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (!string.IsNullOrEmpty(userId))
            {
                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var adminRole = roleManager.FindByName("admin");
                var adminCount = adminRole.Users.Count();
                if (IsAdmin == true)
                {
                    await userManager.AddToRoleAsync(userId, "admin");
                    return RedirectToAction("Index", new { result = "rolAdded" });
                }
                else
                {
                    if (adminCount == 1 && !IsAdmin)
                    {
                        return RedirectToAction("Index", new { result = "OneAdmin" });
                    }

                    await userManager.RemoveFromRolesAsync(userId, "admin");
                    return RedirectToAction("Index", new { result = "rolRemoved" });
                }
            }
            return RedirectToAction("Index", new { result = "errorRolChanging" });
        }

        public async Task<ActionResult> DeleteUser(string id)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", new { result = "userNotFind" });
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Index", new { result = "userNotFind" });
            }
            if (await userManager.IsInRoleAsync(user.Id, "admin"))
            {
                return RedirectToAction("Index", new { result = "adminDeleteError" });
            }

            var articleUser = db.Comments.Where(x => x.ApplicationUserId == id);
            if (articleUser != null)
            {
                db.Comments.RemoveRange(articleUser);
                db.SaveChanges();
            }
    
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index", new { result = "userDeleted" });
        }

        public async Task<ActionResult> PasswordChange(string UserId, string password)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            string errorMessage = null;
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(password))
            {
                errorMessage = "You have to input new password.";
            }
            if (string.IsNullOrEmpty(errorMessage))
            {
                await userManager.RemovePasswordAsync(UserId);
                IdentityResult result = await userManager.AddPasswordAsync(UserId, password);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                else
                {
                    errorMessage = string.Join(" ,", result.Errors);
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { error = errorMessage });
        }

    }
</code>

<strong>View Codes</strong>

```html

@if (!string.IsNullOrEmpty(ViewBag.result))
{
    <div class="alert alert-info" id="alert">
        @switch (ViewBag.result)
        {
            case "rolAdded":
                <p>
                    The administrator authority you selected has been granted.
                </p>
                break;
            case "rolRemoved":
                <p>
                    Admin authority removed from the user you selected
                </p>
                break;
            case "errorRolChanging":
                <p>
                    An error occurred during role assignment.
                </p>
                break;
            case "userDeleted":
                <p>
                    User Has been deleted.
                </p>
                break;
            case "userNotFind":
                <p>
                    User Not Find In Database.
                </p>
                break;
            case "adminDeleteError":
                <p>
                    Admin does not delete.
                </p>
                break;
            case "OneAdmin":
                <p>
                    Must have at least 1 admin
                </p>
                break;
        }
    </div>
}
<div class="container bg-white " style="overflow:scroll">
    <h2 class="text-primary text-center mt-5">User Controller</h2>
    <div>
        <table class="table">
            <thead>
                <tr>
                    <th>User Name</th>
                    <th>Email</th>
                    <th>Roles</th>
                    <th>Authority</th>
                    <th>Delete</th>
                    <th>Update Password</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var user in Model)
                {
                    <tr>
                        <td>@user.UserName</td>
                        <td>@user.Email</td>
                        <td>@string.Join(" ,", user.Roles)</td>
                        <td>
                            @using (Html.BeginForm("GiveAdminRole", "AdminAccount", FormMethod.Post))
                            {
                                @Html.AntiForgeryToken()
                                @Html.Hidden("userId", user.Id)

                                <div class="d-flex">
                                    <label for="IsAdmin-@user.Id">Admin</label>
                                    <div class="ml-1">
                                        @Html.CheckBox("IsAdmin", user.Roles.Contains("admin"), new { id = "IsAdmin-" + user.Id })
                                    </div>
                                </div>
                            }
                        </td>
                        <td><a href="@Url.Action("DeleteUser","AdminAccount",new {id=user.Id})" id="btnDelete"><i class="fas fa-trash-alt tm-trash-icon btn btn-danger"></i></a></td>
                        <td>
                            <button class="btn btn-warning" data-toggle="modal" data-target="#myModal" data-user-id="@user.Id"
                                    data-user-name="@user.UserName">
                                Edit Password
                            </button>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
</div>
<form action="" method="post" id="frmPasswordUpdate">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">Password Update(<span id="UserName"></span>)</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    @Html.AntiForgeryToken()
                    @Html.Hidden("UserId")
                    @Html.Label("New Password")
                    @Html.Password("password", null, new { @class = "form-control", required = "required", minlenght = 6 })
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="submit" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
</form>

```

<strong>Script Codes</strong>

```html
 <script>
        $("input:checkbox[name='IsAdmin']").change(function () {
            $(this).closest("form").submit();
        });

        $("#btnDelete").click(function () {
            if (!confirm("Your user will be delete after this process ?")) {
                return false;
            };
        });

        setTimeout(function () {
            $("#alert").hide(500);
        }, 2000);

        $("#myModal").on("show.bs.modal", function (event) {
            var button = $(event.relatedTarget);
            var userId = button.data("user-id");
            var userName = button.data('user-name');
            var modal = $(this);
            modal.find("#UserId").val(userId);
            modal.find('#UserName').text(userName);
        });

        $("#frmPasswordUpdate").submit(function (e) {
            e.preventDefault();
            var frm = this;
            $.ajax({
                type: "post",
                url: "@Url.Action("PasswordChange","AdminAccount")",
                data: $(frm).serialize(),
                success: function (data) {
                    frm.reset();
                    $("#myModal").modal("hide");
                    alert("Password changed success")
                },
                error: function (xhr, status, error) {
                    return (xhr.responseJSON.error);
                }
            });
        });

    </script>
    
 ```
