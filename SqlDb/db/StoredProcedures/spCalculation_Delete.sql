CREATE PROCEDURE [dbo].[spCalculation_Delete]
	@Id int
AS
begin
	delete from dbo.[Calculations]
	where [Id] = @Id;
end
