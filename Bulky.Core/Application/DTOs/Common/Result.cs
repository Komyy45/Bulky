using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Application.Models.Common
{
	public sealed record Result<T>
	{
		private readonly T? _value;
		private Result(T value)
		{
			Value = value;
			IsSuccess = true;
			Errors = null;
		}
		private Result(params Error[] errors)
		{
			Errors = new();
			foreach (var error in errors)
			{
				if (error == Error.None)
				{
					throw new ArgumentException("invalid error", nameof(error));
				}
				Errors.Add(error);
			}
			IsSuccess = false;
		}
		public bool IsSuccess { get; }
		public bool IsFailure => !IsSuccess;
		public T Value
		{
			get
			{
				if (IsFailure)
				{
					throw new InvalidOperationException("there is no value for failure");
				}
				return _value!;
			}
			private init => _value = value;
		}
		public List<Error>? Errors { get; }
		public static Result<T> Success(T value)
		{
			return new Result<T>(value);
		}
		public static Result<T> Failure(params Error[] errors)
		{
			return new Result<T>(errors);
		}
	}

	public sealed record Result
	{
		private Result()
		{
			IsSuccess = true;
			Errors = null;
		}
		private Result(params Error[] errors)
		{
			Errors = new();
			foreach (var error in errors)
			{
				if (error == Error.None)
				{
					throw new ArgumentException("invalid error", nameof(error));
				}
				Errors.Add(error);
			}
			IsSuccess = false;
		}
		public bool IsSuccess { get; }
		public bool IsFailure => !IsSuccess;

		public List<Error>? Errors { get; }
		public static Result Success()
		{
			return new Result();
		}
		public static Result Failure(params Error[] errors)
		{
			return new Result(errors);
		}
	}
}
