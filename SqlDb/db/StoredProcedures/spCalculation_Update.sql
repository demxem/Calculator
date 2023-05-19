CREATE PROCEDURE [dbo].[spCalculation_Update]
	@Id int,
	@Type nvarchar(50),
	@Expression nvarchar(50),
	@CreationDate Date,
	@Result float
AS

begin
	update dbo.[Calculations]
	set [Type] = @Type, [Expression] = @Expression, [CreationDate] = @CreationDate, [Result] = @Result
	where Id = @Id;
end

