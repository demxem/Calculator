CREATE PROCEDURE [dbo].[spCalculation_Create]
	@Type nvarchar(50),
	@Expression nvarchar(50),
	@CreationDate Date,
	@Result float

AS
begin
	insert into dbo.[Calculations] ([Type], [Expression], [CreationDate], [Result])
	values (@Type, @Expression, @CreationDate, @Result);
end
	
