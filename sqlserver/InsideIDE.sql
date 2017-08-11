USE [InsideIDE]
GO
/****** Object:  StoredProcedure [dbo].[usp_Account_Delete]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_Delete]
	@AccountId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			DELETE FROM [dbo].[tbl_Account]
			WHERE [AccountId] = @AccountId

			SELECT [code] = 0, [msg] = 'delete success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Account_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_GetList]
	@AccountId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1
		[AccountId]
		,[AccountTypeId]
		,[AccountName]
		,[AccountShortName]
		,[AccountLevelId]
		,[ContactName]
		,[Address]
		,[PhoneNumber1]
		,[PhoneNumber2]
		,[PhoneNumber3]
		,[Email]
		,[Website]
		,[Note]
		,[AccountStatus]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpdateUser]
	FROM [dbo].[tbl_Account]
	WHERE [AccountId] = @AccountId
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Account_GetList_AccountType]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170726
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_GetList_AccountType]
	@AccountId int = 0
	,@AccountTypeId int = 0
	,@AccountStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[AccountId]
		,[AccountName]
		,[AccountShortName]
	FROM [dbo].[tbl_Account]
	WHERE ([AccountStatus] = @AccountStatus OR @AccountStatus = 0)
		AND ([AccountId] = @AccountId OR @AccountId = 0)
		AND ([AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
END





GO
/****** Object:  StoredProcedure [dbo].[usp_Account_Insert]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_Insert] 
	@AccountTypeId int
	,@AccountName nvarchar(100)
	,@AccountShortName nvarchar(50)
	,@AccountLevelId int
	,@ContactName nvarchar(100)
	,@Address nvarchar(200)
	,@PhoneNumber1 varchar(50)
	,@PhoneNumber2 varchar(50)
	,@PhoneNumber3 varchar(50)
	,@Email varchar(50)
	,@Website varchar(50)
	,@Note nvarchar(500)
	,@AccountStatus int
	,@CreateDate datetime
	,@CreateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO [dbo].[tbl_Account]
				([AccountTypeId]
				,[AccountName]
				,[AccountShortName]
				,[AccountLevelId]
				,[ContactName]
				,[Address]
				,[PhoneNumber1]
				,[PhoneNumber2]
				,[PhoneNumber3]
				,[Email]
				,[Website]
				,[Note]
				,[AccountStatus]
				,[CreateDate]
				,[CreateUser])
			VALUES
				(@AccountTypeId
				,@AccountName
				,@AccountShortName
				,@AccountLevelId
				,@ContactName
				,@Address
				,@PhoneNumber1
				,@PhoneNumber2
				,@PhoneNumber3
				,@Email
				,@Website
				,@Note
				,@AccountStatus
				,@CreateDate
				,@CreateUser)

			SELECT [code] = 0, [msg] = 'insert success';
		
			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Account_RowTotal]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170725
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_RowTotal]
	@AccountTypeId int = 0
	,@AccountName nvarchar(100) = ''
	,@AccountLevelId int = 0
	,@AccountStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [RowTotal] = COUNT(0)
	FROM [dbo].[tbl_Account]
	WHERE ([AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
	AND ([AccountName] LIKE '%' + @AccountName + '%' OR @AccountName = '')
	AND ([AccountLevelId] = @AccountLevelId OR @AccountLevelId = 0)
	AND ([AccountStatus] = @AccountStatus OR @AccountStatus = 0) 
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Account_Search]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
-- usp_Account_Search 1, 'a', 0, 1, 30, 1
CREATE PROCEDURE [dbo].[usp_Account_Search]
	@AccountTypeId int = 0
	,@AccountName nvarchar(100) = ''
	,@AccountLevelId int = 0
	,@AccountStatus int = 0
	,@RowsPerPage int = 30
	,@PageNumber int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[AccountId]
		,[AccountTypeId]
		,[AccountName]
		,[AccountShortName]
		,[AccountLevelId]
		,[ContactName]
		,[Address]
		,[PhoneNumber1]
		,[PhoneNumber2]
		,[PhoneNumber3]
		,[Email]
		,[Website]
		,[Note]
		,[AccountStatus]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpdateUser]
	FROM [dbo].[tbl_Account]
	WHERE ([AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
	AND ([AccountName] LIKE '%' + @AccountName + '%' OR @AccountName = '')
	AND ([AccountLevelId] = @AccountLevelId OR @AccountLevelId = 0)
	AND ([AccountStatus] = @AccountStatus OR @AccountStatus = 0)
	ORDER BY [AccountId] DESC
	OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
	FETCH NEXT @RowsPerPage ROWS ONLY
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Account_Update]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Account_Update]
	@AccountId int
	,@AccountTypeId int
	,@AccountName nvarchar(100)
	,@AccountShortName nvarchar(50)
	,@AccountLevelId int
	,@ContactName nvarchar(100)
	,@Address nvarchar(200)
	,@PhoneNumber1 varchar(50)
	,@PhoneNumber2 varchar(50)
	,@PhoneNumber3 varchar(50)
	,@Email varchar(50)
	,@Website varchar(50)
	,@Note nvarchar(500)
	,@AccountStatus int
	,@UpdateDate datetime
	,@UpdateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			UPDATE [dbo].[tbl_Account]
			SET [AccountTypeId] = @AccountTypeId
				,[AccountName] = @AccountName
				,[AccountShortName] = @AccountShortName
				,[AccountLevelId] = @AccountLevelId
				,[ContactName] = @ContactName
				,[Address] = @Address
				,[PhoneNumber1] = @PhoneNumber1
				,[PhoneNumber2] = @PhoneNumber2
				,[PhoneNumber3] = @PhoneNumber3
				,[Email] = @Email
				,[Website] = @Website
				,[Note] = @Note
				,[AccountStatus] = @AccountStatus
				,[UpdateDate] = @UpdateDate
				,[UpdateUser] = @UpdateUser
			WHERE [AccountId] = @AccountId

			SELECT [code] = 0, [msg] = 'update success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_AccountLevel_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170712
-- Description:	
-- =============================================
-- usp_AccountLevel_GetList -1, 1
CREATE PROCEDURE [dbo].[usp_AccountLevel_GetList] 
	@AccountLevelId int = 0
	,@AccountTypeId int = 0
	,@AccountLevelStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[AccountLevelId]
		,[AccountLevelName]
	FROM [InsideIDE].[dbo].[tbl_AccountLevel]
	WHERE ([AccountLevelStatus] = @AccountLevelStatus OR @AccountLevelStatus = 0)
		AND ([AccountLevelId] = @AccountLevelId OR @AccountLevelId = 0)
		AND ([AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
END






GO
/****** Object:  StoredProcedure [dbo].[usp_AccountType_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170712
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_AccountType_GetList] 
	@AccountTypeId int = 0
	,@AccountTypeStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[AccountTypeId]
		,[AccountTypeName]
		,[OrderTypeName]
	FROM [dbo].[tbl_AccountType]
	WHERE ([AccountTypeStatus] = @AccountTypeStatus OR @AccountTypeStatus = 0)
		AND ([AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Fee_Delete]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Fee_Delete]
	@FeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			DELETE [dbo].[tbl_Fee]
			WHERE [FeeId] = @FeeId

			SELECT [code] = 0, [msg] = 'delete success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Fee_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Fee_GetList]
	@FeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
    -- Insert statements for procedure here
	SELECT TOP 1
		[FeeId]
		,[FeeTypeId]
		,[Amount]
		,[UserName]
		,[Note]
		,[FeeStatus]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpdateUser]
		,[RowVersion]
	FROM [dbo].[tbl_Fee]
	WHERE [FeeId] = @FeeId
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Fee_Insert]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Fee_Insert] 
	@FeeTypeId int
	,@Amount money
	,@UserName nvarchar(50)
	,@Note nvarchar(500)
	,@FeeStatus int
	,@CreateDate datetime
	,@CreateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO [dbo].[tbl_Fee]
				([FeeTypeId]
				,[Amount]
				,[UserName]
				,[Note]
				,[FeeStatus]
				,[CreateDate]
				,[CreateUser])
			VALUES
				(@FeeTypeId
				,@Amount
				,@UserName
				,@Note
				,@FeeStatus
				,@CreateDate
				,@CreateUser)

			SELECT [code] = 0, [msg] = 'insert success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Fee_RowTotal]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170808
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Fee_RowTotal] 
	@FromDate datetime
	,@ToDate datetime
	,@FeeTypeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @ToDate = @ToDate + 1

	SELECT [RowTotal] = COUNT(0)
	FROM [dbo].[tbl_Fee]
	WHERE [CreateDate] >= @FromDate AND [CreateDate] < @ToDate
	AND ([FeeTypeId] = @FeeTypeId OR @FeeTypeId = 0)
END


GO
/****** Object:  StoredProcedure [dbo].[usp_Fee_Search]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Fee_Search]
	@FromDate datetime = '20170801'
	,@ToDate datetime = '20170809'
	,@FeeTypeId int = 0
	,@RowsPerPage int = 30
	,@PageNumber int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @ToDate = @ToDate + 1

	SELECT
		[FeeId]
		,[FeeTypeId]
		,[Amount]
		,[UserName]
		,[Note]
		,[FeeStatus]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpdateUser]
	FROM [dbo].[tbl_Fee]
	WHERE [CreateDate] >= @FromDate AND [CreateDate] < @ToDate
	AND ([FeeTypeId] = @FeeTypeId OR @FeeTypeId = 0)
	ORDER BY [CreateDate] DESC
	OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
	FETCH NEXT @RowsPerPage ROWS ONLY
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Fee_Update]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170619
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Fee_Update]
	@FeeId int
	,@FeeTypeId int
	,@Amount money
	,@UserName nvarchar(50)
	,@Note nvarchar(500)
	,@FeeStatus int
	,@UpdateDate datetime
	,@UpdateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			UPDATE [dbo].[tbl_Fee]
			SET [FeeTypeId] = @FeeTypeId
				,[Amount] = @Amount
				,[UserName] = @UserName
				,[Note] = @Note
				,[FeeStatus] = @FeeStatus
				,[UpdateDate] = @UpdateDate
				,[UpdateUser] = @UpdateUser
			WHERE [FeeId] = @FeeId

			SELECT [code] = 0, [msg] = 'update success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_FeeType_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170712
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_FeeType_GetList] 
	@FeeTypeId int = 0
	,@FeeTypeStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[FeeTypeId]
		,[FeeTypeName]
		,[FeeTypeStatus]
	FROM [InsideIDE].[dbo].[tbl_FeeType]
	WHERE ([FeeTypeStatus] = @FeeTypeStatus OR @FeeTypeStatus = 0)
	AND ([FeeTypeId] = @FeeTypeId OR @FeeTypeId = 0)
END






GO
/****** Object:  StoredProcedure [dbo].[usp_FeeType_Insert]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170808
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_FeeType_Insert]
	@FeeTypeName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO [dbo].[tbl_FeeType]
				([FeeTypeName]
				,[FeeTypeStatus])
			VALUES
				(@FeeTypeName
				,1)

			SELECT [code] = 0, [msg] = 'insert success';
		
			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[usp_FeeType_RowTotal]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170808
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_FeeType_RowTotal]
	@FeeTypeName nvarchar(100) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT [RowTotal] = COUNT(0)
	FROM [dbo].[tbl_FeeType]
	WHERE [FeeTypeName] LIKE '%' + @FeeTypeName + '%' OR @FeeTypeName = ''
END


GO
/****** Object:  StoredProcedure [dbo].[usp_FeeType_Search]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170808
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_FeeType_Search] 
	@FeeTypeName nvarchar(100) = ''
	,@RowsPerPage int = 30
	,@PageNumber int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT
		[FeeTypeId]
		,[FeeTypeName]
		,[FeeTypeStatus]
	FROM [dbo].[tbl_FeeType]
	WHERE [FeeTypeName] LIKE '%' + @FeeTypeName + '%' OR @FeeTypeName = ''
	ORDER BY [FeeTypeId]
	OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
	FETCH NEXT @RowsPerPage ROWS ONLY
END


GO
/****** Object:  StoredProcedure [dbo].[usp_FeeType_Update]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170808
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_FeeType_Update]
	@FeeTypeId int
	,@FeeTypeName nvarchar(100)
	,@FeeTypeStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			UPDATE [dbo].[tbl_FeeType]
			SET [FeeTypeName] = @FeeTypeName
				,[FeeTypeStatus] = @FeeTypeStatus
			WHERE [FeeTypeId] = @FeeTypeId

			SELECT [code] = 0, [msg] = 'update success';
		
			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[usp_Order_Delete]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170807
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Order_Delete] 
	@OrderId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			DELETE [dbo].[tbl_Order]
			WHERE [OrderId] = @OrderId

			SELECT [code] = 0, [msg] = 'delete success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END




GO
/****** Object:  StoredProcedure [dbo].[usp_Order_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170726
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Order_GetList] 
	@OrderId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1
		[OrderId]
		,[AccountTypeId]
		,[AccountId]
		,[OrderCode]
		,[TransportFee]
		,[Note]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpdateUser]
		,[RowVersion]
	FROM [dbo].[tbl_Order]
	WHERE [OrderId] = @OrderId
END





GO
/****** Object:  StoredProcedure [dbo].[usp_Order_Insert]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170726
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Order_Insert] 
	@AccountTypeId int
	,@AccountId int
	,@OrderCode nvarchar(100)
	,@TransportFee money
	,@Note nvarchar(500)
	,@CreateDate datetime
	,@CreateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OrderId int = 0

	BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO [dbo].[tbl_Order]
				([AccountTypeId]
				,[AccountId]
				,[OrderCode]
				,[TransportFee]
				,[Note]
				,[CreateDate]
				,[CreateUser])
			VALUES
				(@AccountTypeId
				,@AccountId
				,@OrderCode
				,@TransportFee
				,@Note
				,@CreateDate
				,@CreateUser)

			SET @OrderId = SCOPE_IDENTITY()
			SELECT [code] = 0, [msg] = 'insert success;' + CONVERT(varchar(50), @OrderId);
		
			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END





GO
/****** Object:  StoredProcedure [dbo].[usp_Order_RowTotal]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170726
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Order_RowTotal]
	@FromDate datetime = '20170801'
	,@ToDate datetime = '20170808'
	,@AccountTypeId int = 0
	,@AccountName nvarchar(100) = ''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @ToDate = @ToDate + 1

	SELECT [RowTotal] = COUNT(0)
	FROM [dbo].[tbl_Order] A
	INNER JOIN [dbo].[tbl_Account] B ON A.[AccountId] = B.[AccountId]
	INNER JOIN (SELECT [OrderId], [Quantity] = ISNULL(SUM([Quantity]), 0), [Amount] = ISNULL(SUM([Amount]), 0) FROM [dbo].[tbl_OrderDetail] GROUP BY [OrderId]) C ON A.[OrderId] = C.[OrderId]
	WHERE A.[CreateDate] >= @FromDate AND A.[CreateDate] < @ToDate
		AND (A.[AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
		AND (B.[AccountName] LIKE '%' + @AccountName + '%' OR @AccountName = '')
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Order_Search]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170726
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Order_Search]
	@FromDate datetime = '20170801'
	,@ToDate datetime = '20170808'
	,@AccountTypeId int = 0
	,@AccountName nvarchar(100) = ''
	,@RowsPerPage int = 30
	,@PageNumber int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SET @ToDate = @ToDate + 1

	SELECT
		A.[OrderId]
		,A.[AccountTypeId]
		,B.[AccountName]
		,A.[OrderCode]
		,C.[Quantity]
		,C.[Amount]
		,A.[TransportFee]
		,A.[Note]
		,A.[CreateDate]
		,A.[CreateUser]
		,A.[UpdateDate]
		,A.[UpdateUser]
	FROM [dbo].[tbl_Order] A
	INNER JOIN [dbo].[tbl_Account] B ON A.[AccountId] = B.[AccountId]
	INNER JOIN (SELECT [OrderId], [Quantity] = ISNULL(SUM([Quantity]), 0), [Amount] = ISNULL(SUM([Amount]), 0) FROM [dbo].[tbl_OrderDetail] GROUP BY [OrderId]) C ON A.[OrderId] = C.[OrderId]
	WHERE A.[CreateDate] >= @FromDate AND A.[CreateDate] < @ToDate
		AND (A.[AccountTypeId] = @AccountTypeId OR @AccountTypeId = 0)
		AND (B.[AccountName] LIKE '%' + @AccountName + '%' OR @AccountName = '')
	ORDER BY A.[OrderId] DESC
	OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
	FETCH NEXT @RowsPerPage ROWS ONLY
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Order_Update]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170726
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Order_Update] 
	@OrderId int
	,@AccountTypeId int
	,@AccountId int
	,@OrderCode nvarchar(100)
	,@TransportFee money
	,@Note nvarchar(500)
	,@UpdateDate datetime
	,@UpdateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			UPDATE [dbo].[tbl_Order]
			   SET [AccountTypeId] = @AccountTypeId
				  ,[AccountId] = @AccountId
				  ,[OrderCode] = @OrderCode
				  ,[TransportFee] = @TransportFee
				  ,[Note] = @Note
				  ,[UpdateDate] = @UpdateDate
				  ,[UpdateUser] = @UpdateUser
			WHERE [OrderId] = @OrderId

			SELECT [code] = 0, [msg] = 'update success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END





GO
/****** Object:  StoredProcedure [dbo].[usp_OrderDetail_Delete_OrderId]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170807
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_OrderDetail_Delete_OrderId]
	@OrderId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	BEGIN TRANSACTION
		BEGIN TRY
			DELETE [dbo].[tbl_OrderDetail]
			WHERE [OrderId] = @OrderId

			SELECT [code] = 0, [msg] = 'delete success';
		
			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END




GO
/****** Object:  StoredProcedure [dbo].[usp_OrderDetail_GetList_OrderId]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170807
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_OrderDetail_GetList_OrderId]
	@OrderId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[OrderDetailId]
		,[OrderId]
		,[ProductId]
		,[UnitTypeId]
		,[UnitId]
		,[UnitValue]
		,[Quantity]
		,[Price]
		,[Amount]
	FROM [dbo].[tbl_OrderDetail]
	WHERE [OrderId] = @OrderId
END




GO
/****** Object:  StoredProcedure [dbo].[usp_OrderDetail_Insert]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170807
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_OrderDetail_Insert] 
	@OrderId int
    ,@ProductId int
    ,@UnitTypeId int
    ,@UnitId int
    ,@UnitValue float
    ,@Quantity int
    ,@Price money
    ,@Amount money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO [dbo].[tbl_OrderDetail]
				([OrderId]
				,[ProductId]
				,[UnitTypeId]
				,[UnitId]
				,[UnitValue]
				,[Quantity]
				,[Price]
				,[Amount])
			VALUES
				(@OrderId
				,@ProductId
				,@UnitTypeId
				,@UnitId
				,@UnitValue
				,@Quantity
				,@Price
				,@Amount)

			SELECT [code] = 0, [msg] = 'insert success';
		
			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END




GO
/****** Object:  StoredProcedure [dbo].[usp_Product_Delete]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170618
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_Delete]
	@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			DELETE FROM [dbo].[tbl_Product]
			WHERE [ProductId] = @ProductId

			SELECT [code] = 0, [msg] = 'delete success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Product_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170618
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_GetList]
	@ProductId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1
		[ProductId]
		,[ProductTypeId]
		,[ProductCode]
		,[ProductName]
		,[Price]
		,[Length]
		,[Width]
		,[Depth]
		,[Height]
		,[Weigh]
		,[UnitTypeId]
		,[UnitId]
		,[UnitValue]
		,[ImageName]
		,[ImagePath]
		,[Note]
		,[ProductStatus]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpDateUser]
	FROM [dbo].[tbl_Product]
	WHERE [ProductId] = @ProductId
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Product_GetList_ProductType]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDH
-- Create date: 20170618
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_GetList_ProductType]
	@ProductId int = 0
	,@ProductTypeId int = 0
	,@ProductStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[ProductId]
		,[ProductName]
		,[ProductCode] = [ProductName] + ' | ' + [ProductCode]
	FROM [dbo].[tbl_Product]
	WHERE ([ProductStatus] = @ProductStatus OR @ProductStatus = 0)
		AND ([ProductId] = @ProductId OR @ProductId = 0)
		AND ([ProductTypeId] = @ProductTypeId OR @ProductTypeId = 0)
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Product_Insert]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170618
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_Insert] 
	@ProductTypeId int
	,@ProductCode nvarchar(100)
	,@ProductName nvarchar(100)
	,@Price money
	,@Length float
	,@Width float
	,@Depth float
	,@Height float
	,@Weigh float
	,@UnitTypeId int
	,@UnitId int
	,@UnitValue float
	,@ImageName nvarchar(50)
	,@ImagePath nvarchar(200)
	,@Note nvarchar(500)
	,@ProductStatus int
	,@CreateDate datetime
	,@CreateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			INSERT INTO [dbo].[tbl_Product]
				([ProductTypeId]
				,[ProductCode]
				,[ProductName]
				,[Price]
				,[Length]
				,[Width]
				,[Depth]
				,[Height]
				,[Weigh]
				,[UnitTypeId]
				,[UnitId]
				,[UnitValue]
				,[ImageName]
				,[ImagePath]
				,[Note]
				,[ProductStatus]
				,[CreateDate]
				,[CreateUser])
			VALUES
				(@ProductTypeId
				,@ProductCode
				,@ProductName
				,@Price
				,@Length
				,@Width
				,@Depth
				,@Height
				,@Weigh
				,@UnitTypeId
				,@UnitId
				,@UnitValue
				,@ImageName
				,@ImagePath
				,@Note
				,@ProductStatus
				,@CreateDate
				,@CreateUser)

			SELECT [code] = 0, [msg] = 'insert success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Product_RowTotal]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDh
-- Create date: 20170725
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_RowTotal] 
	@ProductTypeId int = 0
	,@ProductCode nvarchar(100) = ''
	,@ProductName nvarchar(100) = ''
	,@ProductStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [RowTotal] = COUNT(0)
	FROM [dbo].[tbl_Product]
	WHERE ([ProductTypeId] = @ProductTypeId OR @ProductTypeId = 0)
	AND ([ProductCode] LIKE '%' + @ProductCode + '%' OR @ProductCode = '')
	AND ([ProductName] LIKE '%' + @ProductName + '%' OR @ProductName = '')
	AND ([ProductStatus] = @ProductStatus OR @ProductStatus = 0)
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Product_Search]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_Search]
	@ProductTypeId int = 0
	,@ProductCode nvarchar(100) = ''
	,@ProductName nvarchar(100) = ''
	,@ProductStatus int = 0
	,@RowsPerPage int = 30
	,@PageNumber int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[ProductId]
		,[ProductTypeId]
		,[ProductCode]
		,[ProductName]
		,[Price]
		,[Length]
		,[Width]
		,[Depth]
		,[Height]
		,[Weigh]
		,[UnitTypeId]
		,[UnitId]
		,[UnitValue]
		,[ImageName]
		,[ImagePath]
		,[Note]
		,[ProductStatus]
		,[CreateDate]
		,[CreateUser]
		,[UpdateDate]
		,[UpDateUser]
	FROM [dbo].[tbl_Product]
	WHERE ([ProductTypeId] = @ProductTypeId OR @ProductTypeId = 0)
	AND ([ProductCode] LIKE '%' + @ProductCode + '%' OR @ProductCode = '')
	AND ([ProductName] LIKE '%' + @ProductName + '%' OR @ProductName = '')
	AND ([ProductStatus] = @ProductStatus OR @ProductStatus = 0)
	ORDER BY [ProductId]
	OFFSET (@PageNumber - 1) * @RowsPerPage ROWS
	FETCH NEXT @RowsPerPage ROWS ONLY
END







GO
/****** Object:  StoredProcedure [dbo].[usp_Product_Update]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170618
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Product_Update]
	@ProductId int
	,@ProductTypeId int
	,@ProductCode nvarchar(100)
	,@ProductName nvarchar(100)
	,@Price money
	,@Length float
	,@Width float
	,@Depth float
	,@Height float
	,@Weigh float
	,@UnitTypeId int
	,@UnitId int
	,@UnitValue float
	,@ImageName nvarchar(50)
	,@ImagePath nvarchar(200)
	,@Note nvarchar(500)
	,@ProductStatus int
	,@UpdateDate datetime
	,@UpdateUser varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
		BEGIN TRY
			UPDATE [dbo].[tbl_Product]
			SET [ProductTypeId] = @ProductTypeId
				,[ProductCode] = @ProductCode
				,[ProductName] = @ProductName
				,[Price] = @Price
				,[Length] = @Length
				,[Width] = @Width
				,[Depth] = @Depth
				,[Height] = @Height
				,[Weigh] = @Weigh
				,[UnitTypeId] = @UnitTypeId
				,[UnitId] = @UnitId
				,[UnitValue] = @UnitValue
				,[ImageName] = @ImageName
				,[ImagePath] = @ImagePath
				,[Note] = @Note
				,[ProductStatus] = @ProductStatus
				,[UpdateDate] = @UpdateDate
				,[UpdateUser] = @UpdateUser
			WHERE [ProductId] = @ProductId

			SELECT [code] = 0, [msg] = 'update success';

			IF @@TRANCOUNT > 0 BEGIN
				COMMIT TRANSACTION;
			END
		END TRY 
		BEGIN CATCH
			IF @@TRANCOUNT > 0 BEGIN
				ROLLBACK TRANSACTION;
			END

			SELECT [code] = ERROR_NUMBER(), [msg] = ERROR_MESSAGE();
		END CATCH
END







GO
/****** Object:  StoredProcedure [dbo].[usp_ProductType_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170712
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_ProductType_GetList] 
	@ProductTypeId int = 0
	,@ProductTypeStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[ProductTypeId]
		,[ProductTypeName]
	FROM [InsideIDE].[dbo].[tbl_ProductType]
	WHERE ([ProductTypeStatus] = @ProductTypeStatus OR @ProductTypeStatus = 0)
		AND ([ProductTypeId] = @ProductTypeId OR @ProductTypeId = 0)
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Status_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170718
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Status_GetList]
	@StatusId int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[StatusId]
		,[StatusName]
		,[RowVersion]
	FROM [InsideIDE].[dbo].[tbl_Status]
	WHERE [StatusId] = @StatusId OR @StatusId = 0
END






GO
/****** Object:  StoredProcedure [dbo].[usp_Unit_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170725
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_Unit_GetList] 
	@UnitId int = 0
	,@UnitTypeId int = 0
	,@UnitStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[UnitId]
		,[UnitTypeId]
		,[UnitName]
		,[UnitStatus]
		,[RowVersion]
	FROM [dbo].[tbl_Unit]
	WHERE ([UnitStatus] = @UnitStatus OR @UnitStatus = 0)
		AND ([UnitId] = @UnitId OR @UnitId = 0)
		AND ([UnitTypeId] = @UnitTypeId OR @UnitTypeId = 0)
END






GO
/****** Object:  StoredProcedure [dbo].[usp_UnitType_GetList]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		LongNDH
-- Create date: 20170725
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[usp_UnitType_GetList] 
	@UnitTypeId int = 0
	,@ProductTypeId int = 0
	,@UnitTypeStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[UnitTypeId]
		,[UnitTypeName]
	FROM [dbo].[tbl_UnitType]
	WHERE ([UnitTypeStatus] = @UnitTypeStatus OR @UnitTypeStatus = 0)
		AND ([UnitTypeId] = @UnitTypeId OR @UnitTypeId = 0)
		AND ([ProductTypeId] = @ProductTypeId OR @ProductTypeId = 0)
END






GO
/****** Object:  Table [dbo].[tbl_Account]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[AccountName] [nvarchar](100) NOT NULL,
	[AccountShortName] [nvarchar](50) NULL,
	[AccountLevelId] [int] NOT NULL,
	[ContactName] [nvarchar](100) NULL,
	[Address] [nvarchar](200) NULL,
	[PhoneNumber1] [varchar](50) NULL,
	[PhoneNumber2] [varchar](50) NULL,
	[PhoneNumber3] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Website] [varchar](50) NULL,
	[Note] [nvarchar](500) NULL,
	[AccountStatus] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [varchar](50) NULL,
	[RowVersion] [timestamp] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_AccountLevel]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_AccountLevel](
	[AccountLevelId] [int] NOT NULL,
	[AccountLevelName] [nvarchar](100) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[AccountLevelStatus] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_AccountLevel] PRIMARY KEY CLUSTERED 
(
	[AccountLevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_AccountType]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_AccountType](
	[AccountTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeName] [nvarchar](100) NOT NULL,
	[OrderTypeName] [nvarchar](100) NOT NULL,
	[AccountTypeStatus] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_AccountType] PRIMARY KEY CLUSTERED 
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Fee]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Fee](
	[FeeId] [int] IDENTITY(1,1) NOT NULL,
	[FeeTypeId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[FeeStatus] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [varchar](50) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_Fee] PRIMARY KEY CLUSTERED 
(
	[FeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_FeeType]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_FeeType](
	[FeeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[FeeTypeName] [nvarchar](100) NOT NULL,
	[FeeTypeStatus] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_FeeType] PRIMARY KEY CLUSTERED 
(
	[FeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Order]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[OrderCode] [nvarchar](100) NOT NULL,
	[TransportFee] [money] NULL,
	[Note] [nvarchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [varchar](50) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OrderDetail]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitTypeId] [int] NOT NULL,
	[UnitId] [int] NOT NULL,
	[UnitValue] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Amount] [money] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Product]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[ProductCode] [nvarchar](100) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Price] [money] NOT NULL,
	[Length] [float] NULL,
	[Width] [float] NULL,
	[Depth] [float] NULL,
	[Height] [float] NULL,
	[Weigh] [float] NULL,
	[UnitTypeId] [int] NOT NULL,
	[UnitId] [int] NOT NULL,
	[UnitValue] [float] NOT NULL,
	[ImageName] [nvarchar](50) NULL,
	[ImagePath] [nvarchar](200) NULL,
	[Note] [nvarchar](500) NULL,
	[ProductStatus] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUser] [varchar](50) NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ProductType]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ProductType](
	[ProductTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeName] [nvarchar](100) NOT NULL,
	[ProductTypeStatus] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_ProductType] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Status]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Status](
	[StatusId] [int] NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[RowVersion] [timestamp] NULL,
 CONSTRAINT [PK_tbl_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Unit]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Unit](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[UnitTypeId] [int] NOT NULL,
	[UnitName] [nvarchar](100) NOT NULL,
	[UnitStatus] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_Unit] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_UnitType]    Script Date: 8/11/2017 5:04:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_UnitType](
	[UnitTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeId] [int] NULL,
	[UnitTypeName] [nvarchar](100) NOT NULL,
	[UnitTypeStatus] [int] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_tbl_UnitType] PRIMARY KEY CLUSTERED 
(
	[UnitTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[tbl_Account] ON 

INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (1, 1, N'Nguyễn Văn A', N'NVA', -1, N'A', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (2, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (3, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (4, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (5, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (6, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (7, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (8, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (9, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (10, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (11, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (12, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (13, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (14, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (15, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (16, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (17, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (18, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (19, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (20, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (21, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (22, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (23, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (24, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (25, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (26, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (27, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (28, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (29, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (30, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (31, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (32, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (33, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (34, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (35, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (36, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (37, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (38, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (39, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (40, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (41, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (42, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (43, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (44, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (45, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (46, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (47, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (48, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (49, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (50, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (51, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (52, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (53, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (54, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (55, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (56, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (57, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (58, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (59, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (60, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (61, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (62, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (63, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (64, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (65, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (66, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (67, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (68, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (69, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (70, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (71, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (72, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (73, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (74, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (75, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (76, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (77, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (78, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (79, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (80, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (81, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (82, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (83, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (84, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (85, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (86, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (87, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (88, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (89, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (90, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (91, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (92, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (93, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (94, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (95, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (96, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (97, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (98, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Account] ([AccountId], [AccountTypeId], [AccountName], [AccountShortName], [AccountLevelId], [ContactName], [Address], [PhoneNumber1], [PhoneNumber2], [PhoneNumber3], [Email], [Website], [Note], [AccountStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (99, 1, N'Nguyễn Văn B', N'NVB', -1, N'B', N'Hồ Chí Minh', N'0901111111', N'0902222222', N'0903333333', N'nguyenvana@gmail.com', N'nguyenvana.com', N'-', 1, CAST(0x0000A7AE00000000 AS DateTime), N'longndh', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[tbl_Account] OFF
INSERT [dbo].[tbl_AccountLevel] ([AccountLevelId], [AccountLevelName], [AccountTypeId], [AccountLevelStatus]) VALUES (-1, N'', 1, 1)
INSERT [dbo].[tbl_AccountLevel] ([AccountLevelId], [AccountLevelName], [AccountTypeId], [AccountLevelStatus]) VALUES (1, N'Đại lý cấp 1', 2, 1)
INSERT [dbo].[tbl_AccountLevel] ([AccountLevelId], [AccountLevelName], [AccountTypeId], [AccountLevelStatus]) VALUES (2, N'Đại lý cấp 2', 2, 1)
SET IDENTITY_INSERT [dbo].[tbl_AccountType] ON 

INSERT [dbo].[tbl_AccountType] ([AccountTypeId], [AccountTypeName], [OrderTypeName], [AccountTypeStatus]) VALUES (1, N'Đối tác', N'Nhập', 1)
INSERT [dbo].[tbl_AccountType] ([AccountTypeId], [AccountTypeName], [OrderTypeName], [AccountTypeStatus]) VALUES (2, N'Khách hàng', N'Xuất', 1)
SET IDENTITY_INSERT [dbo].[tbl_AccountType] OFF
SET IDENTITY_INSERT [dbo].[tbl_Fee] ON 

INSERT [dbo].[tbl_Fee] ([FeeId], [FeeTypeId], [Amount], [UserName], [Note], [FeeStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (1, 1, 3333.0000, N'lognndh1', N'test1', 1, CAST(0x0000A7CA01726D8F AS DateTime), N'longndh', CAST(0x0000A7CA0175E146 AS DateTime), N'longndh')
SET IDENTITY_INSERT [dbo].[tbl_Fee] OFF
SET IDENTITY_INSERT [dbo].[tbl_FeeType] ON 

INSERT [dbo].[tbl_FeeType] ([FeeTypeId], [FeeTypeName], [FeeTypeStatus]) VALUES (1, N'Văn phòng phẩm', 2)
INSERT [dbo].[tbl_FeeType] ([FeeTypeId], [FeeTypeName], [FeeTypeStatus]) VALUES (2, N'Tiếp khách', 1)
INSERT [dbo].[tbl_FeeType] ([FeeTypeId], [FeeTypeName], [FeeTypeStatus]) VALUES (3, N'test', 1)
SET IDENTITY_INSERT [dbo].[tbl_FeeType] OFF
SET IDENTITY_INSERT [dbo].[tbl_Order] ON 

INSERT [dbo].[tbl_Order] ([OrderId], [AccountTypeId], [AccountId], [OrderCode], [TransportFee], [Note], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (1, 1, 1, N'test', 10000.0000, N'', CAST(0x0000A7C9010DD7B0 AS DateTime), N'longndh', NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_Order] OFF
SET IDENTITY_INSERT [dbo].[tbl_OrderDetail] ON 

INSERT [dbo].[tbl_OrderDetail] ([OrderDetailId], [OrderId], [ProductId], [UnitTypeId], [UnitId], [UnitValue], [Quantity], [Price], [Amount]) VALUES (9, 1, 2, 1, 6, 6, 200, 225000.0000, 45000000.0000)
INSERT [dbo].[tbl_OrderDetail] ([OrderDetailId], [OrderId], [ProductId], [UnitTypeId], [UnitId], [UnitValue], [Quantity], [Price], [Amount]) VALUES (10, 1, 3, 3, 3, 120, 300, 120000.0000, 36000000.0000)
INSERT [dbo].[tbl_OrderDetail] ([OrderDetailId], [OrderId], [ProductId], [UnitTypeId], [UnitId], [UnitValue], [Quantity], [Price], [Amount]) VALUES (11, 1, 4, 4, 4, 200, 25, 100000.0000, 2500000.0000)
SET IDENTITY_INSERT [dbo].[tbl_OrderDetail] OFF
SET IDENTITY_INSERT [dbo].[tbl_Product] ON 

INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (1, 1, N'SN001', N'Sàn nhựa A', 150000.0000, 100, 10, 10, 10, 10, 1, 1, 200, N'lf90qyzjezfxm6ov9j4wfs4nti2g37.jpg', N'/Images/Product/lf90qyzjezfxm6ov9j4wfs4nti2g37.jpg', N'', 1, CAST(0x0000A7BD01541AAC AS DateTime), N'longndh', CAST(0x0000A7BD0154F5EE AS DateTime), N'longndh')
INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (2, 1, N'SN002', N'Sàn nhựa B', 225000.0000, 100, 10, 10, 10, 10, 1, 6, 6, N'leqrc291ljuk15hh95se0qtoskwl6z.jpg', N'/Images/Product/leqrc291ljuk15hh95se0qtoskwl6z.jpg', N'', 1, CAST(0x0000A7BD01543411 AS DateTime), N'longndh', CAST(0x0000A7BD01551734 AS DateTime), N'longndh')
INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (3, 1, N'SN003', N'Sàn nhựa C', 120000.0000, 100, 10, 10, 10, 10, 3, 3, 120, N'xddklgfvh75a8t4ofq54qjl14o509d.jpg', N'/Images/Product/xddklgfvh75a8t4ofq54qjl14o509d.jpg', N'', 1, CAST(0x0000A7BD01544F74 AS DateTime), N'longndh', CAST(0x0000A7BD0155286E AS DateTime), N'longndh')
INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (4, 2, N'LEN004', N'Len D', 100000.0000, 100, 10, 10, 10, 10, 4, 4, 200, N'qflgzs4gkgcf6dxb4ehfha6gsldxfa.jpg', N'/Images/Product/qflgzs4gkgcf6dxb4ehfha6gsldxfa.jpg', N'', 1, CAST(0x0000A7BD01546CBB AS DateTime), N'longndh', CAST(0x0000A7BD0154C60C AS DateTime), N'longndh')
INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (5, 3, N'NEP005', N'Nẹp E', 50000.0000, 100, 10, 10, 10, 10, 5, 5, 100, N'0ejunko67fgia6pr1qc6g6rp3pt6zi.jpg', N'/Images/Product/0ejunko67fgia6pr1qc6g6rp3pt6zi.jpg', N'', 1, CAST(0x0000A7BD01548DED AS DateTime), N'longndh', NULL, NULL)
INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (6, 4, N'KEO006', N'Keo F', 10000.0000, 100, 10, 10, 10, 10, 2, 2, 10, N'2jp1nx1tjtysprw1j07vbz0ixzeopg.jpg', N'/Images/Product/2jp1nx1tjtysprw1j07vbz0ixzeopg.jpg', N'', 1, CAST(0x0000A7BD0154A81E AS DateTime), N'longndh', CAST(0x0000A7BD0154CF6D AS DateTime), N'longndh')
INSERT [dbo].[tbl_Product] ([ProductId], [ProductTypeId], [ProductCode], [ProductName], [Price], [Length], [Width], [Depth], [Height], [Weigh], [UnitTypeId], [UnitId], [UnitValue], [ImageName], [ImagePath], [Note], [ProductStatus], [CreateDate], [CreateUser], [UpdateDate], [UpdateUser]) VALUES (7, 4, N'KEO0067', N'Keo G', 10000.0000, 100, 10, 10, 10, 10, 6, 7, 5, N'2zgk4ws8o6rakyl4wvochid9w3mzrz.jpg', N'/Images/Product/2zgk4ws8o6rakyl4wvochid9w3mzrz.jpg', N'', 1, CAST(0x0000A7BD0154B8BF AS DateTime), N'longndh', CAST(0x0000A7BD0154D679 AS DateTime), N'longndh')
SET IDENTITY_INSERT [dbo].[tbl_Product] OFF
SET IDENTITY_INSERT [dbo].[tbl_ProductType] ON 

INSERT [dbo].[tbl_ProductType] ([ProductTypeId], [ProductTypeName], [ProductTypeStatus]) VALUES (1, N'Sàn nhựa', 1)
INSERT [dbo].[tbl_ProductType] ([ProductTypeId], [ProductTypeName], [ProductTypeStatus]) VALUES (2, N'Len', 1)
INSERT [dbo].[tbl_ProductType] ([ProductTypeId], [ProductTypeName], [ProductTypeStatus]) VALUES (3, N'Nẹp', 1)
INSERT [dbo].[tbl_ProductType] ([ProductTypeId], [ProductTypeName], [ProductTypeStatus]) VALUES (4, N'Keo', 1)
SET IDENTITY_INSERT [dbo].[tbl_ProductType] OFF
INSERT [dbo].[tbl_Status] ([StatusId], [StatusName]) VALUES (1, N'Hoạt động')
INSERT [dbo].[tbl_Status] ([StatusId], [StatusName]) VALUES (2, N'Không hoạt động')
SET IDENTITY_INSERT [dbo].[tbl_Unit] ON 

INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (1, 1, N'm2', 1)
INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (2, 2, N'm2', 1)
INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (3, 3, N'm2', 1)
INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (4, 4, N'm', 1)
INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (5, 5, N'm', 1)
INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (6, 1, N'kg', 1)
INSERT [dbo].[tbl_Unit] ([UnitId], [UnitTypeId], [UnitName], [UnitStatus]) VALUES (7, 6, N'kg', 1)
SET IDENTITY_INSERT [dbo].[tbl_Unit] OFF
SET IDENTITY_INSERT [dbo].[tbl_UnitType] ON 

INSERT [dbo].[tbl_UnitType] ([UnitTypeId], [ProductTypeId], [UnitTypeName], [UnitTypeStatus]) VALUES (1, 1, N'Thùng', 1)
INSERT [dbo].[tbl_UnitType] ([UnitTypeId], [ProductTypeId], [UnitTypeName], [UnitTypeStatus]) VALUES (2, 4, N'Thùng', 1)
INSERT [dbo].[tbl_UnitType] ([UnitTypeId], [ProductTypeId], [UnitTypeName], [UnitTypeStatus]) VALUES (3, 1, N'Tấm', 1)
INSERT [dbo].[tbl_UnitType] ([UnitTypeId], [ProductTypeId], [UnitTypeName], [UnitTypeStatus]) VALUES (4, 2, N'Thanh', 1)
INSERT [dbo].[tbl_UnitType] ([UnitTypeId], [ProductTypeId], [UnitTypeName], [UnitTypeStatus]) VALUES (5, 3, N'Thanh', 1)
INSERT [dbo].[tbl_UnitType] ([UnitTypeId], [ProductTypeId], [UnitTypeName], [UnitTypeStatus]) VALUES (6, 4, N'Bịch', 1)
SET IDENTITY_INSERT [dbo].[tbl_UnitType] OFF
